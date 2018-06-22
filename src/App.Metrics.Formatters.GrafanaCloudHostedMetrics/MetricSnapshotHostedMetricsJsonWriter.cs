// <copyright file="MetricSnapshotHostedMetricsJsonWriter.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.Metrics.Formatters.GrafanaCloudHostedMetrics.Internal;
using App.Metrics.Serialization;

namespace App.Metrics.Formatters.GrafanaCloudHostedMetrics
{
    public class MetricSnapshotHostedMetricsJsonWriter : IMetricSnapshotWriter
    {
        private readonly TextWriter _textWriter;
        private readonly IHostedMetricsPointTextWriter _metricPointTextWriter;
        private readonly HostedMetricsPoints _points;

        public MetricSnapshotHostedMetricsJsonWriter(
            TextWriter textWriter,
            Func<IHostedMetricsPointTextWriter> metricPointTextWriter = null)
        {
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
            _points = new HostedMetricsPoints();

            _metricPointTextWriter = metricPointTextWriter != null ? metricPointTextWriter() : HostedMetricsFormatterConstants.GraphiteDefaults.MetricPointTextWriter();
        }

        /// <inheritdoc />
        public void Write(string context, string name, string field, object value, MetricTags tags, DateTime timestamp)
        {
            _points.Add(new HostedMetricsPoint(context, name, new Dictionary<string, object> { { "value", value } }, tags, _metricPointTextWriter, timestamp));
        }

        /// <inheritdoc />
        public void Write(string context, string name, IEnumerable<string> columns, IEnumerable<object> values, MetricTags tags, DateTime timestamp)
        {
            var fields = columns.Zip(values, (column, data) => new { column, data }).ToDictionary(pair => pair.column, pair => pair.data);

            _points.Add(new HostedMetricsPoint(context, name, fields, tags, _metricPointTextWriter, timestamp));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _points.Write(_textWriter);
#if !NETSTANDARD1_6
                _textWriter?.Close();
#endif
                _textWriter?.Dispose();
            }
        }
    }
}
