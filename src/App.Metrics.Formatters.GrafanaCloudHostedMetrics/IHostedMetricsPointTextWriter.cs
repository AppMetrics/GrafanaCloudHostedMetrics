// <copyright file="IHostedMetricsPointTextWriter.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using App.Metrics.Formatters.GrafanaCloudHostedMetrics.Internal;
using Newtonsoft.Json;

namespace App.Metrics.Formatters.GrafanaCloudHostedMetrics
{
    public interface IHostedMetricsPointTextWriter
    {
        void Write(JsonWriter textWriter, HostedMetricsPoint point, bool writeTimestamp = true);
    }
}