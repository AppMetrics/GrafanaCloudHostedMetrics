// <copyright file="IHostedMetricsClient.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace App.Metrics.Reporting.GrafanaCloudHostedMetrics.Client
{
    public interface IHostedMetricsClient
    {
        Task<HostedMetricsWriteResult> WriteAsync(string payload, CancellationToken cancellationToken = default);
    }
}