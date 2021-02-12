using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public static class PlotExtensions
    {
        /// <summary>
        /// Sets the title of the plot.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="title">The new title.</param>
        public static PlotConfigurator WithTitle(this PlotConfigurator plot,string? title)
        {
            plot.Title = title;

            return plot;
        }

        /// <summary>
        /// Adds a <see cref="Series.LineSeries"/> to the plot.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="points">The data for the series.</param>
        public static LineSeriesConfigurator WithLine(this PlotConfigurator plot, IEnumerable<DataPoint> points)
        {
            LineSeriesConfigurator lineSeries = new(plot, points);
            
            plot.Series.Add(lineSeries);

            return lineSeries;
        }

        /// <summary>
        /// Gets the specified axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="direction">The axis direction.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        /// <returns></returns>
        public static AxisConfigurator Axis(this PlotConfigurator plot, AxisDirection direction, bool secondary = false)
        {
            AxisConfigurator? axis = plot.Axes
                .FirstOrDefault(a => a.Direction == direction && a.IsSecondary == secondary);

            if (axis == null)
            {
                axis = new AxisConfigurator(plot, direction, secondary);
                plot.Axes.Add(axis);
            }

            return axis;
        }

        /// <summary>
        /// Gets the x-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        public static AxisConfigurator XAxis(this PlotConfigurator plot, bool secondary = false)
            => plot.Axis(AxisDirection.X, secondary);
        
        /// <summary>
        /// Gets the y-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        public static AxisConfigurator YAxis(this PlotConfigurator plot, bool secondary = false)
            => plot.Axis(AxisDirection.Y, secondary);
    }
}