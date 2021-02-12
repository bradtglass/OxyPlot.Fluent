using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class PlotConfigurator : IFluentInterface
    {
        public List<AxisConfigurator> Axes { get; } = new();

        public string? Title { get; set; }

        public List<SeriesConfigurator> Series { get; } = new();

        public PlotModel Build()
        {
            PlotModel model = new();

            foreach (Axis axis in Axes.Select(a => a.Build())) model.Axes.Add(axis);

            foreach (Series.Series series in Series.Select(s => s.Build())) model.Series.Add(series);

            return model;
        }
    }
}