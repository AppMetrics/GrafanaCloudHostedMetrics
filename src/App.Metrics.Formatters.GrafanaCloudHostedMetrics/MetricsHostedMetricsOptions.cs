// <copyright file="MetricsHostedMetricsOptions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using App.Metrics.Formatters.GrafanaCloudHostedMetrics.Internal;

namespace App.Metrics.Formatters.GrafanaCloudHostedMetrics
{
    /// <summary>
    ///     Provides programmatic configuration for GrafanfaCloud Hosted Metrics format in the App Metrics framework.
    /// </summary>
    public class MetricsHostedMetricsOptions
    {
        public MetricsHostedMetricsOptions()
        {
            MetricNameMapping = new GeneratedMetricNameMapping();
            MetricNameFormatter = HostedMetricsFormatterConstants.GraphiteDefaults.MetricPointTextWriter;
        }

        public Func<IHostedMetricsPointTextWriter> MetricNameFormatter { get; set; }

        public GeneratedMetricNameMapping MetricNameMapping { get; set; }
    }
}
