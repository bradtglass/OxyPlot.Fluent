using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a <see cref="Figure" /> (one or more plots with a layout).
    /// </summary>
    [PublicAPI]
    public sealed class FigureConfigurator : IFluentInterface
    {
        /// <summary>
        ///     The value to set the overall title/window caption to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public string? Title { get; set; }


        /// <summary>
        ///     The number of rows in the plot grid.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        ///     The number of columns in the plot grid.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        ///     The configured plots to display in the figure grid.
        /// </summary>
        public Dictionary<Cell, PlotConfigurator> Plots { get; } = new();

        /// <summary>
        ///     Creates and configures a <see cref="Figure" /> specified by the options in this <see cref="FigureConfigurator" />.
        /// </summary>
        public Figure Build()
        {
            IEnumerable<Plot> plots = Plots.Select(p => new Plot(p.Value.Build(), p.Key));

            return new Figure(plots,
                Title,
                Rows,
                Columns);
        }
    }
}