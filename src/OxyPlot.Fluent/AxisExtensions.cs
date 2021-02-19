using System;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring axes.
    /// </summary>
    [PublicAPI]
    public static class AxisExtensions
    {
        /// <summary>
        ///     Sets the title for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="title">The title.</param>
        public static AxisConfigurator<T> SetTitle<T>(this AxisConfigurator<T> axis, string title)
            where T : Axis =>
            axis.Set(a => a.Title, title);

        /// <summary>
        ///     Sets the tick style for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="style">The style.</param>
        public static AxisConfigurator<T> SetTickStyle<T>(this AxisConfigurator<T> axis, TickStyle style)
            where T : Axis =>
            axis.Set(a => a.TickStyle, style);

        /// <summary>
        ///     Sets the minimum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="minimum">The minimum value.</param>
        public static AxisConfigurator<T> SetMinimum<T>(this AxisConfigurator<T> axis, double minimum)
            where T : Axis =>
            axis.Set(a => a.Minimum, minimum);

        /// <summary>
        ///     Sets the maximum for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="maximum">The maximum value.</param>
        public static AxisConfigurator<T> SetMaximum<T>(this AxisConfigurator<T> axis, double maximum)
            where T : Axis =>
            axis.Set(a => a.Maximum, maximum);

        /// <summary>
        ///     Sets the tick label angle.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="angle">The angle.</param>
        public static AxisConfigurator<T> SetLabelAngle<T>(this AxisConfigurator<T> axis, double angle)
            where T : Axis =>
            axis.Set(a => a.LabelAngle, angle);

        /// <summary>
        ///     Sets the label format string.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="format">The format.</param>
        public static AxisConfigurator<T> SetLabelFormat<T>(this AxisConfigurator<T> axis, string format)
            where T : Axis =>
            axis.Set(a => a.LabelFormat, format);

        /// <summary>
        ///     Configures the major ticks for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the ticks.</param>
        public static AxisConfigurator<T> WithMajorTicks<T>(this AxisConfigurator<T> axis,
            Action<AxisTickConfigurator>? configure = null)
            where T : Axis =>
            axis.With(a => a.MajorTicks, configure);

        /// <summary>
        ///     Sets the minor ticks for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the ticks.</param>
        public static AxisConfigurator<T> WithMinorTicks<T>(this AxisConfigurator<T> axis,
            Action<AxisTickConfigurator>? configure = null)
            where T : Axis =>
            axis.With(a => a.MinorTicks, configure);

        /// <summary>
        ///     Sets the extra gridlines for the axis.
        /// </summary>
        /// <param name="axis">The axis to configure.</param>
        /// <param name="configure">Configures the gridlines.</param>
        public static AxisConfigurator<T> WithExtraGridlines<T>(this AxisConfigurator<T> axis,
            Action<ExtraGridlinesConfigurator>? configure = null)
            where T : Axis =>
            axis.With(a => a.ExtraGridlines, configure);
    }
}