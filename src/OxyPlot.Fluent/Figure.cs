using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public sealed class Figure
    {
        internal Figure(IEnumerable<Plot> plots, string? title, int rows, int columns)
        {
            Plots = plots.ToList();
            Title = title;
            Rows = rows;
            Columns = columns;
        }

        public int Rows { get; }
        public int Columns { get; }

        public IReadOnlyList<Plot> Plots { get; }

        public string? Title { get; }

        public static FigureConfigurator Configure()
            => new();
    }
}