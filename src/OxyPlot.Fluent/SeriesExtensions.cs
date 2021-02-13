using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring series.
    /// </summary>
    [PublicAPI]
    public static class SeriesExtensions
    {
        /// <summary>
        ///     Setup the series to use the secondary Y axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <typeparam name="T">The type of series to configure.</typeparam>
        public static T UseSecondaryYAxis<T>(this T series)
            where T : SeriesConfigurator
        {
            series.UseSecondaryYAxis = true;

            return series;
        }

        /// <summary>
        ///     Setup the series to use the secondary X axis.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <typeparam name="T">The type of series to configure.</typeparam>
        public static T UseSecondaryXAxis<T>(this T series)
            where T : SeriesConfigurator
        {
            series.UseSecondaryXAxis = true;

            return series;
        }

        /// <summary>
        ///     Sets the series title.
        /// </summary>
        /// <param name="series">The series to configure.</param>
        /// <param name="title">The title.</param>
        /// <typeparam name="T">The type of series to configure.</typeparam>
        public static T SetTitle<T>(this T series, string title)
            where T : SeriesConfigurator
        {
            series.Title = title;

            return series;
        }
    }
}