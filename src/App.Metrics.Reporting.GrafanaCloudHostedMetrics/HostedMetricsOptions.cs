// <copyright file="HostedMetricsOptions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;

namespace App.Metrics.Reporting.GrafanaCloudHostedMetrics
{
    public class HostedMetricsOptions
    {
        public HostedMetricsOptions(Uri baseUri, string apiKey)
        {
            BaseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        public HostedMetricsOptions()
        {
        }

        public Uri BaseUri { get; set; }

        public string ApiKey { get; set; }
    }
}
