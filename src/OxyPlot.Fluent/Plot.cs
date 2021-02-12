using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public sealed class Plot
    {
        internal Plot(PlotModel model, Cell cell, string? title)
        {
            Model = model;
            Cell = cell;
            Title = title;
        }

        public PlotModel Model { get; }
        public Cell Cell { get; }
        public string? Title { get; }

        public static PlotConfigurator Configure()
            => new(new FigureConfigurator());
    }
}