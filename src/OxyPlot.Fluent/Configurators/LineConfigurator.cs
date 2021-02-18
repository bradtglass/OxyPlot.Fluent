using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for line or other element with line properties.
    /// </summary>
    [PublicAPI]
    public class LineConfigurator : Configurator, ITriStateConfigurator
    {
        /// <summary>
        ///     Configuration callbacks for a <see cref="LineSeries" />.
        /// </summary>
        public static CallbackSet<LineSeries> LineSeriesCallbacks
            = new((s, c) => s.Color = c,
                (s, t) => s.StrokeThickness = t,
                (s, ls) => s.LineStyle = ls);

        /// <summary>
        ///     Configuration callbacks for an <see cref="Axis" /> minor ticks/gridlines.
        /// </summary>
        public static CallbackSet<Axis> AxisMinorGridlinesCallbacks
            = new((s, c) => s.MinorGridlineColor = c,
                (s, t) => s.MinorGridlineThickness = t,
                (s, ls) => s.MinorGridlineStyle = ls);

        /// <summary>
        ///     Configuration callbacks for an <see cref="Axis" /> major ticks/gridlines.
        /// </summary>
        public static CallbackSet<Axis> AxisMajorGridlinesCallbacks
            = new((s, c) => s.MajorGridlineColor = c,
                (s, t) => s.MajorGridlineThickness = t,
                (s, ls) => s.MajorGridlineStyle = ls);

        /// <summary>
        ///     Configuration callbacks for an <see cref="Axis" /> extra ticks/gridlines.
        /// </summary>
        public static CallbackSet<Axis> AxisExtraGridlinesCallbacks
            = new((s, c) => s.ExtraGridlineColor = c,
                (s, t) => s.ExtraGridlineThickness = t,
                (s, ls) => s.ExtraGridlineStyle = ls);

        /// <summary>
        ///     The value to set the line Color to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<OxyColor> Color { get; } = new();

        /// <summary>
        ///     The value to set the line Thickness to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> Thickness { get; } = new();

        /// <summary>
        ///     The value to set the line Style to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<LineStyle> Style { get; } = new();

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
        ///     Configures a line on an unknown target type.
        /// </summary>
        public void ConfigureLine<T>(T target, CallbackSet<T> callbackSet)
        {
            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Include:
                    Color.ApplyIfSet(target, callbackSet.ColorSetter);
                    Thickness.ApplyIfSet(target, callbackSet.ThicknessSetter);
                    Style.ApplyIfSet(target, callbackSet.StyleSetter);

                    return;
                case ConfigurationState.Exclude:
                    callbackSet.StyleSetter(target, LineStyle.None);

                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Callbacks used to configure a line on an unknown target type.
        /// </summary>
        public class CallbackSet<T>
        {
            /// <summary>
            ///     Create a new <see cref="CallbackSet{T}" />.
            /// </summary>
            public CallbackSet(Action<T, OxyColor> colorSetter,
                Action<T, double> thicknessSetter,
                Action<T, LineStyle> styleSetter)
            {
                ColorSetter = colorSetter;
                ThicknessSetter = thicknessSetter;
                StyleSetter = styleSetter;
            }

            /// <summary>
            ///     A callback to set the line color.
            /// </summary>
            public Action<T, OxyColor> ColorSetter { get; }


            /// <summary>
            ///     A callback to set the line thickness.
            /// </summary>
            public Action<T, double> ThicknessSetter { get; }

            /// <summary>
            ///     A callback to set the line style.
            /// </summary>
            public Action<T, LineStyle> StyleSetter { get; }
        }
    }
}