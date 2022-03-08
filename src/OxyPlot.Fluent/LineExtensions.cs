using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Extension methods for configuring lines.
    /// </summary>
    [PublicAPI]
    public static class LineExtensions
    {
        /// <summary>
        ///     Sets the line style.
        /// </summary>
        /// <param name="line">The line to configure.</param>
        /// <param name="style">The style.</param>
        public static T SetStyle<T>(this T line, LineStyle style)
            where T : LineConfigurator =>
            line.Set(l => l.Style, style);

        /// <summary>
        ///     Sets the line thickness.
        /// </summary>
        /// <param name="line">The line to configure.</param>
        /// <param name="thickness">The thickness.</param>
        public static T SetThickness<T>(this T line, double thickness)
            where T : LineConfigurator =>
            line.Set(l => l.Thickness, thickness);

        /// <summary>
        ///     Sets the line color.
        /// </summary>
        /// <param name="line">The line to configure.</param>
        /// <param name="color">The color.</param>
        public static T SetColor<T>(this T line, OxyColor color)
            where T : LineConfigurator =>
            line.Set(l => l.Color, color);
    }
}