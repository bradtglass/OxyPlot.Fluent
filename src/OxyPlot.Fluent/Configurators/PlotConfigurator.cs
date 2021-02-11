using System.Collections.Generic;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class PlotConfigurator : IFluentInterface
    {
        public PlotConfigurator(FigureConfigurator? figure)
        {
            Figure = figure;
        }

        public FigureConfigurator? Figure { get; }

        public List<AxisConfigurator> XAxes { get; } = new();

        public List<AxisConfigurator> YAxes { get; } = new();

        public string? Title { get; set; }

        public List<SeriesConfigurator> Series { get; } = new();
    }
}