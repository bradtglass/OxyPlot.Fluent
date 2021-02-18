using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a plot legend.
    /// </summary>
    [PublicAPI]
    public sealed class LegendConfigurator : Configurator, ITriStateConfigurator
    {
        /// <summary>
        ///     The value to set <see cref="PlotModel.LegendPlacement" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<LegendPlacement> Placement { get; } = new();

        /// <summary>
        ///     The value to set <see cref="PlotModel.LegendPosition" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<LegendPosition> Position { get; } = new();

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToIncludedState()
            => State = ConfigurationState.Include;

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToNotSetState()
            => State = ConfigurationState.NotSet;

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToExcludedState()
            => State = ConfigurationState.Exclude;

        /// <summary>
        ///     Configures the target <see cref="PlotModel" />.
        /// </summary>
        public void Configure(PlotModel target)
        {
            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Exclude:
                    target.IsLegendVisible = false;
                    return;
                case ConfigurationState.Include:
                    target.IsLegendVisible = true;
                    Placement.ApplyIfSet(target, (m, p) => m.LegendPlacement = p);
                    Position.ApplyIfSet(target, (m, p) => m.LegendPosition = p);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State));
            }
        }
    }
}