using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class AxisTickConfigurator : LineConfigurator
    {
        public string? TickFormat { get; set; }

        public double TickSize { get; set; }

        public TickStyle TickStyle { get; set; }
    }
}