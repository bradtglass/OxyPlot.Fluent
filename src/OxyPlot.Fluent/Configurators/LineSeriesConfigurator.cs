using System.Collections.Generic;
using JetBrains.Annotations;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    
    /// <summary>
    /// Configuration options for a <see cref="LineSeries"/>.
    /// </summary>
    [PublicAPI]
    public class LineSeriesConfigurator : SeriesConfigurator
    {
        /// <summary>
        /// Creates a new <see cref="LineSeries"/> with the provided <paramref name="data"/>.
        /// </summary>
        public LineSeriesConfigurator(IEnumerable<DataPoint> data)
        {
            Data = data;
        }

        /// <summary>
        /// The configuration for the series Line or <see langword="null"/> to skip configuration for it.
        /// </summary>
        public LineConfigurator? Line { get; set; }

        /// <summary>
        /// The configuration for the series Marker or <see langword="null"/> to skip configuration for it.
        /// </summary>
        public MarkerConfigurator? Marker { get; set; }

        /// <summary>
        /// The series data.
        /// </summary>
        public IEnumerable<DataPoint> Data { get; }

        /// <inheritdoc/>
        public override Series.Series Build()
        {
            LineSeries series = new();
            foreach (DataPoint point in Data)
                series.Points.Add(point);

            ConfigureSeries(series);

            if (Line != null)
            {
                ConfiguratorHelper.SetIfNotNull(Line.Colour, c => series.Color = c);
                ConfiguratorHelper.SetIfNotNull(Line.Thickness, t => series.StrokeThickness = t);
                ConfiguratorHelper.SetIfNotNull(Line.Style, s => series.LineStyle = s);
            }

            if (Marker != null)
            {
                ConfiguratorHelper.SetIfNotNull(Marker.Type, t => series.MarkerType = t);
                ConfiguratorHelper.SetIfNotNull(Marker.Fill, f => series.MarkerFill = f);
                ConfiguratorHelper.SetIfNotNull(Marker.Stroke, s => series.MarkerStroke = s);
                ConfiguratorHelper.SetIfNotNull(Marker.StrokeThickness, t => series.MarkerStrokeThickness = t);
                ConfiguratorHelper.SetIfNotNull(Marker.Size, s => series.MarkerSize = s);
            }

            return series;
        }
    }
}