using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class AxisTickConfigurator : LineConfigurator
    {
        public double? TickSize { get; set; }
    }
}