using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class AxisStepConfigurator : AxisTickConfigurator
    {
        public double Step { get; set; }
    }
}