using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for minor or major ticks on a <see cref="Axis" />
    /// </summary>
    [PublicAPI]
    public sealed class AxisTickConfigurator : LineConfigurator
    {
        /// <summary>
        ///     The value to set TickSize to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> TickSize { get; } = new();

        /// <summary>
        ///     The value to set Step.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> Step { get; } = new();

        /// <summary>
        ///     Configures the minor ticks/gridlines using the current state of this configurator.
        /// </summary>
        public void ConfigureMinor(Axis axis)
            => Configure(axis,
                AxisMinorGridlinesCallbacks,
                (a, s) => a.MinorTickSize = s,
                (a, s) => a.MinorStep = s);


        /// <summary>
        ///     Configures the major ticks/gridlines using the current state of this configurator.
        /// </summary>
        public void ConfigureMajor(Axis axis)
            => Configure(axis,
                AxisMajorGridlinesCallbacks,
                (a, s) => a.MajorTickSize = s,
                (a, s) => a.MajorStep = s);

        private void Configure(Axis axis,
            CallbackSet<Axis> lineCallbackSet,
            Action<Axis, double> tickSizeSetter,
            Action<Axis, double> stepSetter)
        {
            ConfigureLine(axis, lineCallbackSet);

            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Include:

                    TickSize.ApplyIfSet(axis, tickSizeSetter);
                    Step.ApplyIfSet(axis, stepSetter);

                    break;
                case ConfigurationState.Exclude:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(State));
            }
        }
    }
}