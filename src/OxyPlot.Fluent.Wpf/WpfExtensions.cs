using System.Windows;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Wpf
{
    /// <summary>
    ///     Extension methods for generating WPF specific components (windows, user controls etc.).
    /// </summary>
    [PublicAPI]
    public static class WpfExtensions
    {
        /// <summary>
        ///     Creates a window for the <paramref name="figure" />.
        /// </summary>
        public static Window AsWindow(this Figure figure)
            => new FigureWindow(figure);
    }
}