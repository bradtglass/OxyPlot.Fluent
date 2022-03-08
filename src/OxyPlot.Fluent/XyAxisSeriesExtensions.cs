using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;
using OxyPlot.Series;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring <see cref="XYAxisSeries" />.
    /// </summary>
    [PublicAPI]
    public static class XyAxisSeriesExtensions
    {
        /// <summary>
        ///     Setup the series to use the secondary Y axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        public static TConfigurator UseSecondaryYAxis<TConfigurator, TSeries>(this TConfigurator series)
            where TConfigurator : XyAxisSeriesConfiguratorBase<TSeries>
            where TSeries : XYAxisSeries
            => series.Set(s => s.UseSecondaryYAxis, true);

        /// <summary>
        ///     Setup the series to use the secondary X axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        public static TConfigurator UseSecondaryXAxis<TConfigurator, TSeries>(this TConfigurator series)
            where TConfigurator : XyAxisSeriesConfiguratorBase<TSeries>
            where TSeries : XYAxisSeries
            => series.Set(s => s.UseSecondaryXAxis, true);
    }
}