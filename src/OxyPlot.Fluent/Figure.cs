using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     A fully configured set of one or more <see cref="Plot" /> arranged in a grid. Equivalent to a
    ///     <see href="https://uk.mathworks.com/help/matlab/ref/matlab.ui.figure-properties.html">Figure</see> in MATLAB.
    /// </summary>
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

        /// <summary>
        ///     The number of rows to include in the plot grid.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        ///     The number of columns to include in the plot grid.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        ///     The configured plots to show in the plot grid.
        /// </summary>
        public IReadOnlyList<Plot> Plots { get; }

        /// <summary>
        ///     The figure title (can also be used as the window title if displayed in a window).
        /// </summary>
        public string? Title { get; }

        /// <summary>
        ///     Begins the configuration of a new <see cref="Figure" />.
        /// </summary>
        public static FigureConfigurator Configure()
            => new();
    }
}