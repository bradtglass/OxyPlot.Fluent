using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a <see cref="Series.Series" />.
    /// </summary>
    [PublicAPI]
    public abstract class SeriesConfigurator : IFluentInterface
    {
        /// <summary>
        ///     A <see langword="bool" /> indicating if this <see cref="Series.Series" /> should be plotted against a secondary Y
        ///     axis.
        /// </summary>
        public bool UseSecondaryYAxis { get; set; }

        /// <summary>
        ///     A <see langword="bool" /> indicating if this <see cref="Series.Series" /> should be plotted against a secondary X
        ///     axis.
        /// </summary>
        public bool UseSecondaryXAxis { get; set; }

        /// <summary>
        ///     The value to set <see cref="Series.Series.Title" /> to or <see langword="null" /> to skip configuring this
        ///     property.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Creates and configures a <see cref="Series.Series" /> specified by the options in this
        ///     <see cref="SeriesConfigurator" />.
        /// </summary>
        public abstract Series.Series Build();

        /// <summary>
        ///     Configures <see cref="Series.Series" /> properties based on the options in this <see cref="SeriesConfigurator" />.
        /// </summary>
        /// <param name="series"></param>
        protected void ConfigureSeries(Series.Series series)
        {
            series.Title = Title;
        }
    }
}