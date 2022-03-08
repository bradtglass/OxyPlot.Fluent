using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring extra gridlines.
    /// </summary>
    [PublicAPI]
    public static class ExtraGridlinesExtensions
    {
        /// <summary>
        ///     Sets the gridline positions.
        /// </summary>
        /// <param name="gridlines">The gridlines to configure.</param>
        /// <param name="ticks">The positions.</param>
        public static ExtraGridlinesConfigurator SetTicks(this ExtraGridlinesConfigurator gridlines, double[] ticks)
            => gridlines.Set(g => g.Ticks, ticks);
    }
}