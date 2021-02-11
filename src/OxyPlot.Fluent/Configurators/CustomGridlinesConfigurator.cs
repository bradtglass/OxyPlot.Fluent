using System.Collections.Generic;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class CustomGridlinesConfigurator : LineConfigurator
    {
        public IReadOnlyList<double>? Ticks { get; set; }

        public IReadOnlyList<string>? TickLabels { get; set; }
    }
}