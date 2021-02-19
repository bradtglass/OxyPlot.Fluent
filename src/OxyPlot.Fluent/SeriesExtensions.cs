using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring <see cref="Series.Series" />.
    /// </summary>
    [PublicAPI]
    public static class SeriesExtensions
    {
        /// <summary>
        ///     Sets the series title.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="title">The title.</param>
        public static TConfigurator SetTitle<TConfigurator, TSeries>(this TConfigurator series, string title)
            where TConfigurator : SeriesConfigurator<TSeries>
            where TSeries : Series.Series
            =>
            series.Set(s => s.Title, title);
    }
}