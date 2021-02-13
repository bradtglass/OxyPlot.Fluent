using System;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring figures.
    /// </summary>
    [PublicAPI]
    public static class FigureExtensions
    {
        /// <summary>
        /// Sets the figure title.
        /// </summary>
        /// <param name="figure">The figure to configure.</param>
        /// <param name="title">The title.</param>
        public static FigureConfigurator SetTitle(this FigureConfigurator figure, string title)
        {
            figure.Title = title;

            return figure;
        }
        
        /// <summary>
        ///     Configures the figure for multiple plots in a grid format. Equivalent to subplot or tiledlayout in MATLAB.
        /// </summary>
        /// <param name="figure">The figure to configure.</param>
        /// <param name="x">Number of columns in the grid.</param>
        /// <param name="y">Number of rows in the grid.</param>
        public static FigureConfigurator UsePlotGrid(this FigureConfigurator figure, int x, int y)
        {
            figure.Columns = x;
            figure.Rows = y;

            return figure;
        }

        /// <summary>
        ///     Creates a new plot at the specified index in the figure grid. Equivalent to nexttile in MATLAB.
        /// </summary>
        /// <remarks>
        ///     This method will automatically expand the grid if <paramref name="x" /> or <paramref name="y" /> are greater than
        ///     the current grid size allows.
        /// </remarks>
        /// <param name="figure">The figure to configure.</param>
        /// <param name="x">The zero-based column in the grid.</param>
        /// <param name="y">The zero-based row in the grid.</param>
        /// <param name="configure">Configures the plot.</param>
        public static FigureConfigurator WithPlot(this FigureConfigurator figure, int x, int y,
            Action<PlotConfigurator>? configure = null)
        {
            if (ThrowHelper.NegativeArgument(x, nameof(x), out Exception? negativeXException))
                throw negativeXException;

            if (ThrowHelper.NegativeArgument(y, nameof(y), out Exception? negativeYException))
                throw negativeYException;

            if (figure.Columns < x + 1)
                figure.Columns = x + 1;

            if (figure.Rows < y + 1)
                figure.Rows = y + 1;

            PlotConfigurator plot = new();
            figure.Plots[new Cell(x, y)] = plot;

            configure?.Invoke(plot);

            return figure;
        }

        /// <summary>
        ///     Creates a new plot at 0, 0 in the grid.
        /// </summary>
        /// <param name="figure">The figure to configure.</param>
        /// <param name="configure">Configures the plot.</param>
        public static FigureConfigurator WithPlot(this FigureConfigurator figure,
            Action<PlotConfigurator>? configure = null)
            => figure.WithPlot(0, 0, configure);
    }
}