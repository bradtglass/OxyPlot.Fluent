using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public static class AxisExtensions
    {
        /// <summary>
        /// Sets the minimum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="minimum">The minimum value.</param>
        public static AxisConfigurator WithMinimum(this AxisConfigurator axis, double? minimum)
        {
            axis.Minimum = minimum;

            return axis;
        }
        
        /// <summary>
        /// Sets the maximum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="maximum">The maximum value.</param>
        public static AxisConfigurator WithMaximum(this AxisConfigurator axis, double? maximum)
        {
            axis.Maximum = maximum;

            return axis;
        }
    }
}