using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Non-generic configuration options for a <see cref="XYAxisSeries" />. For use in styling, to configure a specific
    ///     series type implement a configurator for it by inheriting from <see cref="XyAxisSeriesConfiguratorBase{T}" />.
    /// </summary>
    [PublicAPI]
    public class XyAxisSeriesConfigurator : XyAxisSeriesConfiguratorBase<XYAxisSeries>
    {
        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(XYAxisSeries target)
        {
            ConfigureXyAxisSeries(target);
        }
    }
}