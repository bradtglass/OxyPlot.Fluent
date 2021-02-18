using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for <see cref="Series" /> marker.
    /// </summary>
    [PublicAPI]
    public sealed class MarkerConfigurator : Configurator, ITriStateConfigurator
    {
        /// <summary>
        ///     Configuration callbacks for a <see cref="LineSeries" />.
        /// </summary>
        public static CallbackSet<LineSeries> LineSeriesCallbacks
            = new((ls, t) => ls.MarkerType = t,
                (ls, s) => ls.MarkerSize = s,
                (ls, f) => ls.MarkerFill = f,
                (ls, s) => ls.MarkerStroke = s,
                (ls, t) => ls.MarkerStrokeThickness = t);

        /// <summary>
        ///     The value to set the marker Type to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<MarkerType> Type { get; } = new();

        /// <summary>
        ///     The value to set the marker Size to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> Size { get; } = new();

        /// <summary>
        ///     The value to set the marker Fill to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<OxyColor> Fill { get; } = new();

        /// <summary>
        ///     The value to set the marker Stroke to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<OxyColor> Stroke { get; } = new();

        /// <summary>
        ///     The value to set the marker StrokeThickness to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<double> StrokeThickness { get; } = new();

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
        ///     Configures a marker on an unknown target type.
        /// </summary>
        public void ConfigureMarker<T>(T target, CallbackSet<T> callbackSet)
        {
            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Include:
                    Type.ApplyIfSet(target, callbackSet.TypeSetter);
                    Size.ApplyIfSet(target, callbackSet.SizeSetter);
                    Fill.ApplyIfSet(target, callbackSet.FillSetter);
                    Stroke.ApplyIfSet(target, callbackSet.StrokeSetter);
                    StrokeThickness.ApplyIfSet(target, callbackSet.StrokeThicknessSetter);

                    return;
                case ConfigurationState.Exclude:
                    callbackSet.TypeSetter(target, MarkerType.None);

                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Callbacks used to configure a marker on an unknown target type.
        /// </summary>
        public class CallbackSet<T>
        {
            /// <summary>
            ///     Create a new <see cref="CallbackSet{T}" />.
            /// </summary>
            public CallbackSet(Action<T, MarkerType> typeSetter,
                Action<T, double> sizeSetter,
                Action<T, OxyColor> fillSetter,
                Action<T, OxyColor> strokeSetter,
                Action<T, double> strokeThicknessSetter)
            {
                TypeSetter = typeSetter;
                SizeSetter = sizeSetter;
                FillSetter = fillSetter;
                StrokeSetter = strokeSetter;
                StrokeThicknessSetter = strokeThicknessSetter;
            }

            /// <summary>
            ///     A callback to set the marker type.
            /// </summary>
            public Action<T, MarkerType> TypeSetter { get; }

            /// <summary>
            ///     A callback to set the size.
            /// </summary>
            public Action<T, double> SizeSetter { get; }

            /// <summary>
            ///     A callback to set the fill.
            /// </summary>
            public Action<T, OxyColor> FillSetter { get; }

            /// <summary>
            ///     A callback to set the stroke.
            /// </summary>
            public Action<T, OxyColor> StrokeSetter { get; }

            /// <summary>
            ///     A callback to set the stroke thickness.
            /// </summary>
            public Action<T, double> StrokeThicknessSetter { get; }
        }
    }
}