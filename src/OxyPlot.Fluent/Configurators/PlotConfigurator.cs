using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a <see cref="Plot" />/<see cref="PlotModel" />.
    /// </summary>
    [PublicAPI]
    public sealed class PlotConfigurator : BuildableConfigurator<PlotModel, PlotModel>
    {
        /// <inheritdoc />
        public PlotConfigurator()
        {
            State = ConfigurationState.Include;
        }
        
        /// <summary>
        ///     The configurations for each axis. If the configuration for a required axis does not exist then it will not be
        ///     configured but will still be created in the <see cref="PlotModel" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<IAxisConfigurator> Axes { get; } = new();

        /// <summary>
        ///     The value to set <see cref="PlotModel.Title" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<string> Title { get; } = new();

        /// <summary>
        ///     The configurations for each <see cref="OxyPlot.Series.Series" /> to include in the <see cref="Plot" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public List<ISeriesConfigurator> Series { get; } = new();

        /// <summary>
        ///     The configuration for the Legend or <see langword="null" /> to not show a legend.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public LegendConfigurator Legend { get; } = new();

        private static Axis CreateAxis(AxisPosition position)
        {
            LinearAxis axis = new()
            {
                Position = position,
                Key = AxisPositionConfigurator.CalculateKey(position)
            };

            return axis;
        }

        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(PlotModel target)
        {
            Title.ApplyIfSet(target, (p, t) => p.Title = t);

            foreach (Series.Series series in Series.Select(s => s.Build())) target.Series.Add(series);

            foreach (IAxisConfigurator axisConfigurator in Axes)
            {
                // Axes in the list cannot be in a not included state
                if (axisConfigurator.Position.State != ConfigurationState.Include)
                    throw new InvalidOperationException("Cannot configure an axis without a configured position");

                // If the axis is already present on the plot configure it instead of building it
                if (target.Axes.FirstOrDefault(a => a.Position == axisConfigurator.Position.GetPosition()) is
                        { } axis &&
                    axis.GetType() == axisConfigurator.ConcreteAxisType)
                    axisConfigurator.Configure(axis);
                // If not, build a new one 
                else
                    target.Axes.Add(axisConfigurator.Build());
            }

            // Check an axis has been created for every position required by the series, if not add a new one
            foreach (AxisPosition position in Series.SelectMany(s => new[] {s.GetXPosition(), s.GetYPosition()})
                .Where(p => p.HasValue)
                .Select(p => p!.Value)
                .Distinct()
                .Except(target.Axes.Select(a => a.Position)))
                target.Axes.Add(CreateAxis(position));

            Legend.Configure(target);
        }
    }
}