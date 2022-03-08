using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring line series.
    /// </summary>
    [PublicAPI]
    public static class LegendExtensions
    {
        /// <summary>
        ///     Sets the legend position.
        /// </summary>
        /// <param name="legend">The legend to configure.</param>
        /// <param name="position">The position.</param>
        public static LegendConfigurator SetPosition(this LegendConfigurator legend, LegendPosition position)
            => legend.Set(l => l.Position, position);

        /// <summary>
        ///     Sets the legend placement.
        /// </summary>
        /// <param name="legend">The legend to configure.</param>
        /// <param name="placement">The placement.</param>
        public static LegendConfigurator SetPlacement(this LegendConfigurator legend, LegendPlacement placement)
            => legend.Set(l => l.Placement, placement);
    }
}