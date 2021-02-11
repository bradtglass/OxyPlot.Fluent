using System.Collections.Generic;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class FigureConfigurator : IFluentInterface
    {
        public string? Title { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public Dictionary<Cell, PlotConfigurator> Plots { get; } = new();
    }
}