using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Non-generic configuration options for a <see cref="Series" />. For use in styling, to configure a specific
    ///     series type implement a configurator for it by inheriting from <see cref="SeriesConfiguratorBase{T}" />.
    /// </summary>
    [PublicAPI]
    public class SeriesConfigurator : SeriesConfiguratorBase<Series.Series>
    {
        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(Series.Series target)
        {
            ConfigureSeries(target);
        }

        /// <inheritdoc />
        public override AxisPosition? GetXPosition()
            => null;

        /// <inheritdoc />
        public override AxisPosition? GetYPosition()
            => null;
    }
}