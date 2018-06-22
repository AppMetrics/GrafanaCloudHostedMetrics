// <copyright file="MetricsHostedMetricsJsonOutputFormatter.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Serialization;
#if !NETSTANDARD1_6
using App.Metrics.Internal;
#endif

namespace App.Metrics.Formatters.GrafanaCloudHostedMetrics
{
    public class MetricsHostedMetricsJsonOutputFormatter : IMetricsOutputFormatter
    {
        private readonly MetricsHostedMetricsOptions _options;

        public MetricsHostedMetricsJsonOutputFormatter()
        {
            _options = new MetricsHostedMetricsOptions();
        }

        public MetricsHostedMetricsJsonOutputFormatter(MetricsHostedMetricsOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public MetricsMediaTypeValue MediaType => new MetricsMediaTypeValue("text", "vnd.appmetrics.metrics.hostedmetrics", "v1", "plain");

        /// <inheritdoc />
        public MetricFields MetricFields { get; set; }

        /// <inheritdoc />
        public Task WriteAsync(
            Stream output,
            MetricsDataValueSource metricsData,
            CancellationToken cancellationToken = default)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var serializer = new MetricSnapshotSerializer();

            using (var streamWriter = new StreamWriter(output))
            {
                using (var textWriter = new MetricSnapshotHostedMetricsJsonWriter(
                    streamWriter,
                    _options.MetricNameFormatter))
                {
                    serializer.Serialize(textWriter, metricsData, MetricFields);
                }
            }

#if !NETSTANDARD1_6
            return AppMetricsTaskHelper.CompletedTask();
#else
            return Task.CompletedTask;
#endif
        }
    }
}
