// <copyright file="HostedMetricsFormatterConstants.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;

namespace App.Metrics.Formatters.GrafanaCloudHostedMetrics.Internal
{
    public static class HostedMetricsFormatterConstants
    {
        public static class GraphiteDefaults
        {
            public static readonly Func<IHostedMetricsPointTextWriter> MetricPointTextWriter = () => new DefaultHostedMetricsPointTextWriter();
        }
    }
}