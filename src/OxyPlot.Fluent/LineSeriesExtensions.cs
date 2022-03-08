using System;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;
using OxyPlot.Series;

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
            => series.With(s => s.Line, configure);

        /// <summary>
        ///     Configure the marker.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="configure">Configures the marker.</param>
        public static LineSeriesConfigurator WithMarker(this LineSeriesConfigurator series,
            Action<MarkerConfigurator>? configure)
            => series.With(s => s.Marker, configure);
        
        /// <summary>
        ///     Sets the series title.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="title">The title.</param>
        public static LineSeriesConfigurator SetTitle(this LineSeriesConfigurator series, string title)
            => series.SetTitle<LineSeriesConfigurator, LineSeries>(title);
        
        /// <summary>
        ///     Setup the series to use the secondary Y axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        public static LineSeriesConfigurator UseSecondaryYAxis(this LineSeriesConfigurator series)
            => series.UseSecondaryYAxis<LineSeriesConfigurator, LineSeries>();
        
        /// <summary>
        ///     Setup the series to use the secondary X axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        public static LineSeriesConfigurator UseSecondaryXAxis(this LineSeriesConfigurator series)
            => series.UseSecondaryXAxis<LineSeriesConfigurator, LineSeries>();
        
        /// <summary>
        /// Configures the <paramref name="series"/> using the information in <paramref name="configurator"/>.
        /// </summary>
        /// <param name="series">The <see cref="LineSeries"/> to configure.</param>
        /// <param name="configurator">The configuration to apply.</param>
        public static void Configure(this LineSeries series, LineSeriesConfigurator configurator)
            => configurator.Configure(series);

        /// <summary>
        /// Configures the <paramref name="series"/> using a configurator with options set in <paramref name="configure"/>.
        /// </summary>
        /// <param name="series">The <see cref="LineSeries"/> to configure.</param>
        /// <param name="configure">Sets options on the configurator to apply to the <paramref name="series"/>.</param>
        /// <returns>The fully configured configurator that was used to configure <paramref name="series"/>.</returns>
        public static LineSeriesConfigurator Configure(this LineSeries series,
            Action<LineSeriesConfigurator> configure)
        {
            LineSeriesConfigurator configurator = new();
            configure(configurator);

            series.Configure(configurator);

            return configurator;
        }
    }
}