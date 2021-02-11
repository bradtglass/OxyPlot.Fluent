using System.Collections.Generic;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class LineSeriesConfigurator : SeriesConfigurator
    {
        public LineSeriesConfigurator(PlotConfigurator plot) : base(plot) { }

        public LineConfigurator Line { get; } = new();

        public MarkerConfigurator Marker { get; } = new();

        public IEnumerable<DataPoint> Data { get; set; }
    }
}