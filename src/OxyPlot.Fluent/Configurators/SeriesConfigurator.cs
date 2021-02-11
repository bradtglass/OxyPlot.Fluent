using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class SeriesConfigurator : IFluentInterface
    {
        public SeriesConfigurator(PlotConfigurator plot)
        {
            Plot = plot;
        }

        public PlotConfigurator Plot { get; }

        public bool UseSecondaryYAxis { get; set; }

        public bool UseSecondaryXAxis { get; set; }

        public string? Title { get; set; }
    }
}