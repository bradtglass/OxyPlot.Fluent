using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring axis ticks.
    /// </summary>
    [PublicAPI]
    public static class AxisTickExtensions
    {
        /// <summary>
        ///     Sets the tick size.
        /// </summary>
        /// <param name="axisTick">The axis ticks to configure.</param>
        /// <param name="size">The size.</param>
        public static AxisTickConfigurator SetTickSize(this AxisTickConfigurator axisTick, double size)
        {
            axisTick.TickSize = size;

            return axisTick;
        }

        /// <summary>
        ///     Sets the tick step.
        /// </summary>
        /// <param name="axisTick">The axis ticks to configure.</param>
        /// <param name="step">The step.</param>
        public static AxisTickConfigurator SetStep(this AxisTickConfigurator axisTick, double step)
        {
            axisTick.Step = step;

            return axisTick;
        }
    }
}