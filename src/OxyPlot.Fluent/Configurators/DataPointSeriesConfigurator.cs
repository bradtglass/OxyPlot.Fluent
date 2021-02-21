using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Non-generic configuration options for a <see cref="DataPointSeries" />. For use in styling, to configure a specific
    ///     series implement a configurator for it by inheriting from <see cref="DataPointSeriesConfiguratorBase{T}" />.
    /// </summary>
    [PublicAPI]
    public class DataPointSeriesConfigurator : DataPointSeriesConfiguratorBase<DataPointSeries>
    {
        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(DataPointSeries target)
        {
            ConfigureDataPointSeries(target);
        }
    }
}