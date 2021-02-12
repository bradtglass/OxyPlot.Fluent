using System.Windows;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Wpf
{
    [PublicAPI]
    public static class WpfExtensions
    {
        public static Window AsWindow(this Figure figure)
            => new FigureWindow
            {
                DataContext = figure
            };
    }
}