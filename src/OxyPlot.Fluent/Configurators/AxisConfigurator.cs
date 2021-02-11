using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class AxisConfigurator : IFluentInterface
    {
        public AxisConfigurator(PlotConfigurator plot)
        {
            Plot = plot;
        }

        public PlotConfigurator Plot { get; }

        public double? Minimum { get; set; }

        public double? Maximum { get; set; }

        public double TickLabelRotation { get; set; }

        public string? Title { get; set; }

        public AxisStepConfigurator? MajorTicks { get; set; }

        public AxisStepConfigurator? MinorTicks { get; set; }

        public CustomGridlinesConfigurator? CustomGridlines { get; set; }
    }
}