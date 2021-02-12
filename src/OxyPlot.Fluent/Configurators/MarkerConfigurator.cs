using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class MarkerConfigurator : IFluentInterface
    {
        public MarkerType? Type { get; set; }

        public double? Size { get; set; }

        public OxyColor? Fill { get; set; }

        public OxyColor? Stroke { get; set; }

        public double? StrokeThickness { get; set; }
    }
}