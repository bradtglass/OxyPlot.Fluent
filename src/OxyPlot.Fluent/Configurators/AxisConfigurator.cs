using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for an <see cref="Axis" />.
    /// </summary>
    [PublicAPI]
    public class AxisConfigurator<T> : BuildableConfigurator<T, Axis>, IAxisConfigurator
        where T : Axis
    {
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public AxisPositionConfigurator Position { get; } = new();
        
        /// <inheritdoc />
        public Type ConcreteAxisType { get; } = typeof(T);

        /// <summary>
        ///     The value to set <see cref="Axis.Minimum" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> Minimum { get; } = new();

        /// <summary>
        ///     The value to set <see cref="Axis.Maximum" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> Maximum { get; } = new();

        /// <summary>
        ///     The value to set <see cref="Axis.Angle" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> LabelAngle { get; } = new();

        /// <summary>
        ///     The value to set <see cref="Axis.StringFormat" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<string?> LabelFormat { get; } = new();

        /// <summary>
        ///     The value to set <see cref="Axis.Title" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<string?> Title { get; } = new();

        /// <summary>
        ///     The configuration for the MajorTicks.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public AxisTickConfigurator MajorTicks { get; } = new();

        /// <summary>
        ///     The configuration for the MinorTicks.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public AxisTickConfigurator MinorTicks { get; } = new();

        /// <summary>
        ///     The configuration for the <see cref="Axis.ExtraGridlines" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ExtraGridlinesConfigurator ExtraGridlines { get; } = new();

        /// <summary>
        ///     The value to set <see cref="Axis.TickStyle" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<TickStyle> TickStyle { get; } = new();

        /// <inheritdoc />
        protected override void ConfigureImplementedProperties(T target)
        {
            Title.ApplyIfSet(target, (a, t) => a.Title = t);
            LabelFormat.ApplyIfSet(target, (a, f) => a.StringFormat = f);
            Minimum.ApplyIfSet(target, (a, m) => a.Minimum = m);
            Maximum.ApplyIfSet(target, (a, m) => a.Maximum = m);
            LabelAngle.ApplyIfSet(target, (a, an) => a.Angle = an);
            TickStyle.ApplyIfSet(target, (a, s) => a.TickStyle = s);

            MajorTicks.ConfigureMajor(target);
            MinorTicks.ConfigureMinor(target);
            ExtraGridlines.Configure(target);
            Position.Configure(target, true);
        }
        
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Configure(Axis target)
            => base.Configure((T) target);
    }
}