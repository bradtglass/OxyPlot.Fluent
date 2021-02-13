using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    
    /// <summary>
    /// Configuration options for minor or major ticks on a <see cref="Axis"/>
    /// </summary>
    [PublicAPI]
    public sealed class AxisTickConfigurator : LineConfigurator
    {
        
        /// <summary>
        /// The value to set TickSize to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public double? TickSize { get; set; }

        
        /// <summary>
        /// The value to set Step to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public double? Step { get; set; }
    }
}