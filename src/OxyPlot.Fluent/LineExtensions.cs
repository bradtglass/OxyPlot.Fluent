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
            where T : LineConfigurator
        {
            line.Style = style;

            return line;
        }

        /// <summary>
        ///     Sets the line thickness.
        /// </summary>
        /// <param name="line">The line to configure.</param>
        /// <param name="thickness">The thickness.</param>
        public static T SetThickness<T>(this T line, double thickness)
            where T : LineConfigurator
        {
            line.Thickness = thickness;

            return line;
        }

        /// <summary>
        ///     Sets the line colour.
        /// </summary>
        /// <param name="line">The line to configure.</param>
        /// <param name="colour">The colour.</param>
        public static T SetColour<T>(this T line, OxyColor colour)
            where T : LineConfigurator
        {
            line.Colour = colour;

            return line;
        }
    }
}