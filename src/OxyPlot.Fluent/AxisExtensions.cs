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

        /// <summary>
        /// Configures the <paramref name="axis"/> using the information in <paramref name="configurator"/>.
        /// </summary>
        /// <param name="axis">The <see cref="Axis"/> to configure.</param>
        /// <param name="configurator">The configuration to apply.</param>
        /// <typeparam name="T">The <see cref="Axis"/> type.</typeparam>
        public static void Configure<T>(this T axis, AxisConfigurator<T> configurator)
            where T : Axis
            => configurator.Configure(axis);
        
        /// <summary>
        /// Configures the <paramref name="axis"/> using a configurator with options set in <paramref name="configure"/>.
        /// </summary>
        /// <param name="axis">The <see cref="Axis"/> to configure.</param>
        /// <param name="configure">Sets options on the configurator to apply to the <paramref name="axis"/>.</param>
        /// <typeparam name="T">The <see cref="Axis"/> type.</typeparam>
        /// <returns>The fully configured configurator that was used to configure <paramref name="axis"/>.</returns>
        public static AxisConfigurator<T> Configure<T>(this T axis,
            Action<AxisConfigurator<T>> configure)
            where T : Axis
        {
            AxisConfigurator<T> configurator = new();
            configure(configurator);

            axis.Configure(configurator);

            return configurator;
        }
    }
}