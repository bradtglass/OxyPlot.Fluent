using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring markers.
    /// </summary>
    [PublicAPI]
    public static class MarkerExtensions
    {
        /// <summary>
        ///     Sets the marker type.
        /// </summary>
        /// <param name="marker">The marker to configure.</param>
        /// <param name="type">The marker type.</param>
        public static MarkerConfigurator SetType(this MarkerConfigurator marker, MarkerType type)
        {
            marker.Type = type;

            return marker;
        }

        /// <summary>
        ///     Sets the marker size.
        /// </summary>
        /// <param name="marker">The marker to configure.</param>
        /// <param name="size">The size.</param>
        public static MarkerConfigurator SetSize(this MarkerConfigurator marker, double size)
        {
            marker.Size = size;

            return marker;
        }

        /// <summary>
        ///     Sets the marker fill color.
        /// </summary>
        /// <param name="marker">The marker to configure.</param>
        /// <param name="fill">The fill color.</param>
        public static MarkerConfigurator SetFill(this MarkerConfigurator marker, OxyColor fill)
        {
            marker.Fill = fill;

            return marker;
        }

        /// <summary>
        ///     Sets the marker stroke color.
        /// </summary>
        /// <param name="marker">The marker to configure.</param>
        /// <param name="stroke">The stroke color.</param>
        public static MarkerConfigurator SetStroke(this MarkerConfigurator marker, OxyColor stroke)
        {
            marker.Stroke = stroke;

            return marker;
        }

        /// <summary>
        ///     Sets the marker stroke thickness.
        /// </summary>
        /// <param name="marker">The marker to configure.</param>
        /// <param name="thickness">The stroke thickness.</param>
        public static MarkerConfigurator SetStrokeThickness(this MarkerConfigurator marker, double thickness)
        {
            marker.StrokeThickness = thickness;

            return marker;
        }
    }
}