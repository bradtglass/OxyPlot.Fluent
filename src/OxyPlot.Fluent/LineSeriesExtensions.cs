using System;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring line series.
    /// </summary>
    [PublicAPI]
    public static class LineSeriesExtensions
    {
        /// <summary>
        ///     Configure the line.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="configure">Configures the line.</param>
        public static LineSeriesConfigurator WithLine(this LineSeriesConfigurator series,
            Action<LineConfigurator>? configure)
        {
            series.Line ??= new LineConfigurator();

            configure?.Invoke(series.Line);

            return series;
        }

        /// <summary>
        ///     Configure the marker.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="configure">Configures the marker.</param>
        public static LineSeriesConfigurator WithMarker(this LineSeriesConfigurator series,
            Action<MarkerConfigurator>? configure)
        {
            series.Marker ??= new MarkerConfigurator();

            configure?.Invoke(series.Marker);

            return series;
        }
    }
}