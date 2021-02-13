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
        ///     The value to set <see cref="Axis.ExtraGridlines" /> to or <see langword="null" /> to skip configuring this
        ///     property.
        /// </summary>
        public double[]? Ticks { get; set; }
    }
}