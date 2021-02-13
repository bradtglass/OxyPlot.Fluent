using System;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public static class AxisExtensions
    {
        /// <summary>
        ///     Sets the title for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="title">The title.</param>
        public static AxisConfigurator SetTitle(this AxisConfigurator axis, string title)
        {
            axis.Title = title;

            return axis;
        }
        
        /// <summary>
        ///     Sets the tick style for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="style">The style.</param>
        public static AxisConfigurator SetTickStyle(this AxisConfigurator axis, TickStyle style)
        {
            axis.TickStyle = style;

            return axis;
        }

        /// <summary>
        ///     Sets the minimum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="minimum">The minimum value.</param>
        public static AxisConfigurator SetMinimum(this AxisConfigurator axis, double minimum)
        {
            axis.Minimum = minimum;

            return axis;
        }

        /// <summary>
        ///     Sets the maximum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="maximum">The maximum value.</param>
        public static AxisConfigurator SetMaximum(this AxisConfigurator axis, double maximum)
        {
            axis.Maximum = maximum;

            return axis;
        }

        /// <summary>
        ///     Sets the tick label angle.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="angle">The angle.</param>
        public static AxisConfigurator SetLabelAngle(this AxisConfigurator axis, double angle)
        {
            axis.LabelAngle = angle;

            return axis;
        }

        /// <summary>
        ///     Sets the label format string.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="format">The format.</param>
        public static AxisConfigurator SetLabelFormat(this AxisConfigurator axis, string format)
        {
            axis.LabelFormat = format;

            return axis;
        }

        /// <summary>
        ///     Configures the major ticks for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the ticks.</param>
        public static AxisConfigurator WithMajorTicks(this AxisConfigurator axis,
            Action<AxisTickConfigurator>? configure = null)
        {
            axis.MajorTicks ??= new AxisTickConfigurator();

            configure?.Invoke(axis.MajorTicks);

            return axis;
        }

        /// <summary>
        ///     Sets the minor ticks for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the ticks.</param>
        public static AxisConfigurator WithMinorTicks(this AxisConfigurator axis,
            Action<AxisTickConfigurator>? configure = null)
        {
            axis.MinorTicks ??= new AxisTickConfigurator();

            configure?.Invoke(axis.MinorTicks);

            return axis;
        }

        /// <summary>
        ///     Sets the custom gridlines for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the gridlines.</param>
        public static AxisConfigurator WithCustomGridlines(this AxisConfigurator axis,
            Action<CustomGridlinesConfigurator>? configure = null)
        {
            axis.CustomGridlines ??= new CustomGridlinesConfigurator();

            configure?.Invoke(axis.CustomGridlines);

            return axis;
        }
    }
}