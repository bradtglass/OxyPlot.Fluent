using System.Collections.Generic;
using System.Linq;
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

        public Figure Build()
        {
            IEnumerable<Plot> plots = Plots.Select(p => new Plot(p.Value.Build(), p.Key, p.Value.Title));

            return new Figure(plots,
                Title,
                Rows,
                Columns);
        }
    }
}