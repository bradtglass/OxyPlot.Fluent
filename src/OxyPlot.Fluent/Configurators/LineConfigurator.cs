using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for line or other element with line properties.
    /// </summary>
    [PublicAPI]
    public class LineConfigurator : IFluentInterface
    {
        /// <summary>
        ///     The value to set the line Color to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public OxyColor? Colour { get; set; }


        /// <summary>
        ///     The value to set the line Thickness to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public double? Thickness { get; set; }


        /// <summary>
        ///     The value to set the line Style to or <see langword="null" /> to skip configuring this property.
        /// </summary>
        public LineStyle? Style { get; set; }
    }
}