// <copyright file="Host.cs" company="App Metrics Contributors">
// Copyright (c) App Metrics Contributors. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Health;
using App.Metrics.Reporting.GrafanaCloudHostedMetrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace GrafanaCloudHostedMetricsSandboxMvc
{
    public static class Host
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            ConfigureLogging();

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                                .AddUserSecrets(typeof(Host).Assembly)
                                .Build();

            var grafanaCloudHostedMetricsOptions = new MetricsReportingHostedMetricsOptions();
            configuration.GetSection(nameof(MetricsReportingHostedMetricsOptions)).Bind(grafanaCloudHostedMetricsOptions);

            // Samples with weight of less than 10% of average should be discarded when rescaling
            const double minimumSampleWeight = 0.001;

            return WebHost.CreateDefaultBuilder(args)
                          .ConfigureMetricsWithDefaults(
                              builder =>
                              {
                                  builder.SampleWith.ForwardDecaying(
                                      AppMetricsReservoirSamplingConstants.DefaultSampleSize,
                                      AppMetricsReservoirSamplingConstants.DefaultExponentialDecayFactor,
                                      minimumSampleWeight: minimumSampleWeight);
                                  builder.Report.ToHostedMetrics(grafanaCloudHostedMetricsOptions);
                              })
                          .ConfigureHealthWithDefaults(
                              (context, services, builder) =>
                              {
                                  builder.OutputHealth.AsPlainText()
                                         .OutputHealth.AsJson()
                                         .HealthChecks.AddCheck("check 1", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy()))
                                         .HealthChecks.AddCheck("check 2", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy()))
                                         .HealthChecks.AddCheck("check 3", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy()))
                                         .RecordResultsAsMetrics(services, TimeSpan.FromSeconds(10));
                              })
                          .UseMetrics()
                          .UseSerilog()
                          .UseStartup<Startup>()
                          .Build();
        }

        public static void Main(string[] args) { BuildWebHost(args).Run(); }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .WriteTo.LiterateConsole()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }
    }
}