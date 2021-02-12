using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public abstract class SeriesConfigurator : IFluentInterface
    {
        protected SeriesConfigurator(PlotConfigurator plot)
        {
            Plot = plot;
        }

        public PlotConfigurator Plot { get; }

        public bool UseSecondaryYAxis { get; set; }

        public bool UseSecondaryXAxis { get; set; }

        public string? Title { get; set; }

        public abstract Series.Series Build();
    }
}