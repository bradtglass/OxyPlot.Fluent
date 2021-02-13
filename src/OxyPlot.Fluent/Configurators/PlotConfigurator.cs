using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    /// Configuration options for a <see cref="Plot"/>/<see cref="PlotModel"/>.
    /// </summary>
    [PublicAPI]
    public sealed class PlotConfigurator : IFluentInterface
    {
        /// <summary>
        /// The configurations for each axis. If the configuration for a required axis does not exist then it will not be configured but will still be created in the <see cref="PlotModel"/>.
        /// </summary>
        public List<AxisConfigurator> Axes { get; } = new();

        /// <summary>
        /// The value to set <see cref="PlotModel.Title"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public string? Title { get; set; }

        
        /// <summary>
        /// The configurations for each <see cref="OxyPlot.Series.Series"/> to include in the <see cref="Plot"/>.
        /// </summary>
        public List<SeriesConfigurator> Series { get; } = new();

        /// <summary>
        /// The configuration for the Legend or <see langword="null"/> to not show a legend.
        /// </summary>
        public LegendConfigurator? Legend { get; set; }

        /// <summary>
        /// Creates and configures an <see cref="PlotModel"/> specified by the options in this <see cref="PlotConfigurator"/>.
        /// </summary>
        public PlotModel Build()
        {
            PlotModel model = new();

            List<Tuple<AxisConfigurator, Axis>> axes = Axes.Select(a => new Tuple<AxisConfigurator, Axis>(a, a.Build()))
                .ToList();

            foreach (SeriesConfigurator seriesConfigurator in Series)
            {
                Series.Series series = seriesConfigurator.Build();

                if (series is XYAxisSeries lineSeries)
                {
                    lineSeries.XAxisKey = FindOrCreateAxis(axes, AxisDirection.X, seriesConfigurator.UseSecondaryXAxis)
                        .Key;
                    lineSeries.YAxisKey = FindOrCreateAxis(axes, AxisDirection.Y, seriesConfigurator.UseSecondaryXAxis)
                        .Key;
                }

                model.Series.Add(series);
            }

            foreach (Axis axis in axes.Select(a => a.Item2))
                model.Axes.Add(axis);

            if (Legend != null)
            {
                model.IsLegendVisible = true;
                ConfigureLegend(model, Legend);
            }
            else
            {
                model.IsLegendVisible = false;
            }

            return model;
        }

        private static void ConfigureLegend(PlotModel model, LegendConfigurator legend)
        {
            ConfiguratorHelper.SetIfNotNull(legend.Placement, p => model.LegendPlacement = p);
            ConfiguratorHelper.SetIfNotNull(legend.Position, p => model.LegendPosition = p);
        }

        private static Axis FindOrCreateAxis(List<Tuple<AxisConfigurator, Axis>> axes,
            AxisDirection direction,
            bool secondary)
        {
            Axis? axis = axes.Where(a => a.Item1.Direction == direction &&
                                         a.Item1.IsSecondary == secondary)
                .Select(a => a.Item2)
                .FirstOrDefault();

            if (axis != null)
                return axis;

            AxisConfigurator configurator = new(direction, secondary);
            axis = configurator.Build();

            axes.Add(new Tuple<AxisConfigurator, Axis>(configurator, axis));

            return axis;
        }
    }
}