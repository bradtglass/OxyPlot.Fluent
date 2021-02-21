using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a <see cref="LineSeries" />.
    /// </summary>
    [PublicAPI]
    public class LineSeriesConfigurator : DataPointSeriesConfiguratorBase<LineSeries>
    {
        /// <summary>
        ///     The configuration for the series Line.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LineConfigurator Line { get; } = new();

        /// <summary>
        ///     The configuration for the series Marker.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public MarkerConfigurator Marker { get; } = new();

        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(LineSeries target)
        {
            ConfigureDataPointSeries(target);

            Line.ConfigureLine(target, LineConfigurator.LineSeriesCallbacks);
            Marker.ConfigureMarker(target, MarkerConfigurator.LineSeriesCallbacks);
        }
    }
}