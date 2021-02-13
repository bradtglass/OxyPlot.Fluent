using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public sealed class CustomGridlinesConfigurator : LineConfigurator
    {
        public double[]? Ticks { get; set; }
    }
}