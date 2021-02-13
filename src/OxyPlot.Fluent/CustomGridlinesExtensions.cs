using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring custom gridlines.
    /// </summary>
    [PublicAPI]
    public static class CustomGridlinesExtensions
    {
        /// <summary>
        ///     Sets the gridline positions.
        /// </summary>
        /// <param name="gridlines">The gridlines to configure.</param>
        /// <param name="ticks">The positions.</param>
        public static CustomGridlinesConfigurator SetTicks(this CustomGridlinesConfigurator gridlines, double[] ticks)
        {
            gridlines.Ticks = ticks;

            return gridlines;
        }
    }
}