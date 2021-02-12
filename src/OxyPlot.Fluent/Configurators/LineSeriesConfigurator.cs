using System.Collections.Generic;
using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class LineSeriesConfigurator : SeriesConfigurator
    {
        public LineSeriesConfigurator(IEnumerable<DataPoint> data)
        {
            Data = data;
        }

        public LineConfigurator Line { get; } = new();

        public MarkerConfigurator Marker { get; } = new();

        public IEnumerable<DataPoint> Data { get; }

        public override Series.Series Build()
        {
            LineSeries series = new();
            foreach (DataPoint point in Data) series.Points.Add(point);

            ConfiguratorHelper.SetIfNotNull(Line.Colour, c => series.Color = c);
            ConfiguratorHelper.SetIfNotNull(Line.Thickness, t => series.StrokeThickness = t);
            ConfiguratorHelper.SetIfNotNull(Line.Style, s => series.LineStyle = s);
            ConfiguratorHelper.SetIfNotNull(Marker.Type, t => series.MarkerType = t);
            ConfiguratorHelper.SetIfNotNull(Marker.Fill, f => series.MarkerFill = f);
            ConfiguratorHelper.SetIfNotNull(Marker.Stroke, s => series.MarkerStroke = s);
            ConfiguratorHelper.SetIfNotNull(Marker.StrokeThickness, t => series.MarkerStrokeThickness = t);
            ConfiguratorHelper.SetIfNotNull(Marker.Size, s => series.MarkerSize = s);

            return series;
        }
    }
}