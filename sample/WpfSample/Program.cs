﻿using System;
using System.Collections.Generic;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Fluent;
using OxyPlot.Fluent.Configurators;
using OxyPlot.Fluent.Wpf;
using OxyPlot.Series;

namespace WpfSample
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Window window = Figure.Configure()
                .SetTitle("A sample figure")
                .WithPlot(p => p
                    .SetTitle("Sample Plot")
                    .WithLineSeries(GetData(), s => s
                        .SetTitle("Weird Data")
                        .WithMarker(m => m
                            .SetType(MarkerType.Circle)
                            .SetFill(OxyColors.Transparent)
                            .SetStroke(OxyColors.Blue)
                            .SetStrokeThickness(2))
                        .WithLine(l => l
                            .SetStyle(LineStyle.Dot)
                            .SetColor(OxyColors.DarkRed))
                        .SetAdditional<LineSeriesConfigurator, LineSeries, string>(ls => ls.TrackerFormatString, "A value of {4} at x={2}"))
                    .WithLegend()
                    .WithXAxis<LinearAxis>(a => a
                        .SetTitle("X Data"))
                    .WithYAxis<LinearAxis>(a => a
                        .SetTitle("(values)")
                        .SetTickStyle(TickStyle.None)))
                .Build()
                .AsWindow();

            window.ShowDialog();
        }

        private static IEnumerable<DataPoint> GetData()
        {
            yield return new DataPoint(0, 0);
            yield return new DataPoint(4, 3);
            yield return new DataPoint(6, 6);
            yield return new DataPoint(7.4, 3);
            yield return new DataPoint(8, 2);
        }
    }
}