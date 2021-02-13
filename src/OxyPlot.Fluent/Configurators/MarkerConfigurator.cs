using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for <see cref="Series.Series" /> marker.
    /// </summary>
    [PublicAPI]
    public sealed class MarkerConfigurator : IFluentInterface
    {
        /// <summary>
        ///     The value to set the marker Type to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public MarkerType? Type { get; set; }

        /// <summary>
        ///     The value to set the marker Size to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public double? Size { get; set; }

        /// <summary>
        ///     The value to set the marker Fill to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public OxyColor? Fill { get; set; }

        /// <summary>
        ///     The value to set the marker Stroke to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public OxyColor? Stroke { get; set; }

        /// <summary>
        ///     The value to set the marker StrokeThickness to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public double? StrokeThickness { get; set; }
    }
}