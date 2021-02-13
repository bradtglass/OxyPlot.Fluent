using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public abstract class SeriesConfigurator : IFluentInterface
    {
        public bool UseSecondaryYAxis { get; set; }

        public bool UseSecondaryXAxis { get; set; }

        public string? Title { get; set; }

        public abstract Series.Series Build();

        protected void ConfigureSeries(Series.Series series)
        {
            series.Title = Title;
        }
    }
}