using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    
    /// <summary>
    /// Configuration options for a plot legend.
    /// </summary>
    [PublicAPI]
    public sealed class LegendConfigurator : IFluentInterface
    {
        
        /// <summary>
        /// The value to set <see cref="PlotModel.LegendPlacement"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public LegendPlacement? Placement { get; set; }

        /// <summary>
        /// The value to set <see cref="PlotModel.LegendPosition"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public LegendPosition? Position { get; set; }
    }
}