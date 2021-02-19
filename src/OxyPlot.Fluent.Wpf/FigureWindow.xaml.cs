using System;

namespace OxyPlot.Fluent.Wpf
{
    /// <summary>
    ///     A window for displaying one or more plots in a grid.
    /// </summary>
    public partial class FigureWindow
    {
        /// <summary>
        ///     Instantiates a new <see cref="FigureWindow" />.
        /// </summary>
        public FigureWindow(Figure figure, bool disposeFigureOnClose = true)
        {
            DataContext = figure;

            if (disposeFigureOnClose)
                Closed += (_, _) => figure.Dispose();

            InitializeComponent();
        }
    }
}