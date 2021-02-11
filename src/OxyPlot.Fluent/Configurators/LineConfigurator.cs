using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class LineConfigurator : IFluentInterface
    {
        public OxyColor Colour { get; set; }

        public double Thickness { get; set; }

        public LineStyle Style { get; set; }
    }
}