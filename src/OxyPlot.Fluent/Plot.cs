using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     A fully configured <see cref="PlotModel" /> with some minor additional metadata to help with layout in a
    ///     <see cref="Figure" />. Partially equivalent to a
    ///     <see href="https://uk.mathworks.com/help/matlab/ref/matlab.graphics.axis.axes-properties.html">Axes</see> in
    ///     MATLAB.
    /// </summary>
    [PublicAPI]
    public sealed class Plot
    {
        internal Plot(PlotModel model, Cell cell)
        {
            Model = model;
            Cell = cell;
        }

        /// <summary>
        ///     The fully configured <see cref="PlotModel" /> to use for displaying the plot data.
        /// </summary>
        public PlotModel Model { get; }

        /// <summary>
        ///     The location to display this plot in a grid.
        /// </summary>
        public Cell Cell { get; }

        /// <summary>
        ///     Begins the configuration of a new <see cref="Plot" />.
        /// </summary>
        public static PlotConfigurator Configure()
            => new();
    }
}