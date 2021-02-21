using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Inheritable configuration options for a <see cref="DataPointSeries" />.
    /// </summary>
    [PublicAPI]
    public abstract class DataPointSeriesConfiguratorBase<T> : XyAxisSeriesConfiguratorBase<T>
        where T : DataPointSeries
    {
        /// <summary>
        ///     The series data.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<IEnumerable<DataPoint>> Data { get; } = new();

        /// <summary>
        ///     Configures properties specific to a <see cref="DataPointSeries" />.
        /// </summary>
        protected void ConfigureDataPointSeries(DataPointSeries target)
        {
            ConfigureXyAxisSeries(target);

            if (Data.IsSet)
            {
                target.Points.Clear();

                if (Data.Value is { } points)
                    foreach (DataPoint point in points)
                        target.Points.Add(point);
            }
        }
    }
}