using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring plots.
    /// </summary>
    [PublicAPI]
    public static class PlotExtensions
    {
        /// <summary>
        ///     Sets the title of the plot.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="title">The new title.</param>
        public static PlotConfigurator SetTitle(this PlotConfigurator plot, string title)
        {
            plot.Title = title;

            return plot;
        }

        /// <summary>
        ///     Adds a <see cref="Series.LineSeries" /> to the plot.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="points">The data for the series.</param>
        /// <param name="configure">Configures the line.</param>
        public static PlotConfigurator WithLine(this PlotConfigurator plot, IEnumerable<DataPoint> points,
            Action<LineSeriesConfigurator>? configure = null)
        {
            LineSeriesConfigurator lineSeries = new(points);

            plot.Series.Add(lineSeries);

            configure?.Invoke(lineSeries);

            return plot;
        }

        /// <summary>
        ///     Gets the specified axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="direction">The axis direction.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        /// <param name="configure">Configures the axis.</param>
        public static PlotConfigurator WithAxis(this PlotConfigurator plot, AxisDirection direction, bool secondary,
            Action<AxisConfigurator>? configure = null)
        {
            AxisConfigurator? axis = plot.Axes
                .FirstOrDefault(a => a.Direction == direction && a.IsSecondary == secondary);

            if (axis == null)
            {
                axis = new AxisConfigurator(direction, secondary);
                plot.Axes.Add(axis);
            }

            configure?.Invoke(axis);

            return plot;
        }

        /// <summary>
        ///     Gets the x-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        /// <param name="configure">Configures the axis.</param>
        public static PlotConfigurator WithXAxis(this PlotConfigurator plot, bool secondary,
            Action<AxisConfigurator>? configure = null)
            => plot.WithAxis(AxisDirection.X, secondary, configure);

        /// <summary>
        ///     Gets the y-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="secondary">Indicates if the secondary axis should be retrieved.</param>
        /// <param name="configure">Configures the axis.</param>
        public static PlotConfigurator WithYAxis(this PlotConfigurator plot, bool secondary,
            Action<AxisConfigurator>? configure = null)
            => plot.WithAxis(AxisDirection.Y, secondary, configure);

        /// <summary>
        ///     Gets the primary x-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="configure">Configures the axis.</param>
        public static PlotConfigurator WithXAxis(this PlotConfigurator plot, Action<AxisConfigurator>? configure = null)
            => plot.WithXAxis(false, configure);

        /// <summary>
        ///     Gets the primary y-axis.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="configure">Configures the axis.</param>
        public static PlotConfigurator WithYAxis(this PlotConfigurator plot, Action<AxisConfigurator>? configure = null)
            => plot.WithYAxis(false, configure);

        /// <summary>
        ///     Configures the plot to show a legend.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        /// <param name="configure">Configures the legend.</param>
        public static PlotConfigurator WithLegend(this PlotConfigurator plot,
            Action<LegendConfigurator>? configure = null)
            => plot.With(p => p.Legend, configure);

        /// <summary>
        ///     Configures the plot to not show a legend.
        /// </summary>
        /// <param name="plot">The plot to configure.</param>
        public static PlotConfigurator WithoutLegend(this PlotConfigurator plot)
            => plot.Without(p => p.Legend);
    }
}