using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for <see cref="Axis.ExtraGridlines" />.
    /// </summary>
    [PublicAPI]
    public sealed class ExtraGridlinesConfigurator : LineConfigurator
    {
        /// <summary>
        ///     The value to set <see cref="Axis.ExtraGridlines" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double[]?> Ticks { get; } = new();

        /// <summary>
        ///     Configures the extra gridlines of the target axis.
        /// </summary>
        public void Configure(Axis target)
        {
            ConfigureLine(target, AxisExtraGridlinesCallbacks);

            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Include:
                    Ticks.ApplyIfSet(target, (a, t) => a.ExtraGridlines = t);

                    return;
                case ConfigurationState.Exclude:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}