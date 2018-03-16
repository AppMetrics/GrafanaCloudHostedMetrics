// <copyright file="HostedMetricsWriteResult.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

namespace App.Metrics.Reporting.GrafanaCloudHostedMetrics.Client
{
    public struct HostedMetricsWriteResult
    {
        public HostedMetricsWriteResult(bool success)
        {
            Success = success;
            ErrorMessage = null;
        }

        public HostedMetricsWriteResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool Success { get; }

        public static readonly HostedMetricsWriteResult SuccessResult = new HostedMetricsWriteResult(true);
    }
}