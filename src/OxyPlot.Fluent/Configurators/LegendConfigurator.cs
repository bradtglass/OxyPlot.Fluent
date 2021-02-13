using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public sealed class LegendConfigurator : IFluentInterface
    {
        public LegendPlacement? Placement { get; set; }

        public LegendPosition? Position { get; set; }
    }
}