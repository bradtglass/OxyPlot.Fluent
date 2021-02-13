using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public sealed class AxisTickConfigurator : LineConfigurator
    {
        public double? TickSize { get; set; }

        public double? Step { get; set; }
    }
}