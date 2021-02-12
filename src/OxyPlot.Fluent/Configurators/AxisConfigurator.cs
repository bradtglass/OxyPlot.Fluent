using System;
using System.Linq;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    [PublicAPI]
    public class AxisConfigurator : IFluentInterface
    {
        public AxisConfigurator(PlotConfigurator plot, AxisDirection direction, bool isSecondary)
        {
            Plot = plot;
            Direction = direction;
            IsSecondary = isSecondary;
        }

        public AxisDirection Direction { get; }

        public bool IsSecondary { get; }

        public PlotConfigurator Plot { get; }

        public double? Minimum { get; set; }

        public double? Maximum { get; set; }

        public double TickLabelRotation { get; set; }

        public string? TickLabelFormat { get; set; }

        public string? Title { get; set; }

        public AxisStepConfigurator? MajorTicks { get; set; }

        public AxisStepConfigurator? MinorTicks { get; set; }

        public CustomGridlinesConfigurator? CustomGridlines { get; set; }

        public TickStyle? TickStyle { get; set; }

        public Axis Build()
        {
            LinearAxis axis = new()
            {
                Position = GetPosition(),
                Angle = TickLabelRotation,
                Title = Title
            };

            ConfiguratorHelper.SetIfNotNull(Minimum, m => axis.Minimum = m);
            ConfiguratorHelper.SetIfNotNull(Maximum, m => axis.Maximum = m);

            if (MajorTicks != null)
                ConfigureTicks(axis, MajorTicks,
                    (l, c) => l.MajorGridlineColor = c,
                    (l, t) => l.MajorGridlineThickness = t,
                    (l, s) => l.MajorGridlineStyle = s,
                    (l, s) => l.MajorTickSize = s,
                    (l, s) => l.MajorStep = s);

            if (MinorTicks != null)
                ConfigureTicks(axis, MinorTicks,
                    (l, c) => l.MinorGridlineColor = c,
                    (l, t) => l.MinorGridlineThickness = t,
                    (l, s) => l.MinorGridlineStyle = s,
                    (l, s) => l.MinorTickSize = s,
                    (l, s) => l.MinorStep = s);

            if (CustomGridlines != null)
                ConfigureCustomGridlines(axis, CustomGridlines);

            return axis;
        }

        private void ConfigureCustomGridlines(LinearAxis axis, CustomGridlinesConfigurator gridlines)
        {
            ConfigureLine(axis, gridlines,
                (a, c) => a.ExtraGridlineColor = c,
                (a, t) => a.ExtraGridlineThickness = t,
                (a, s) => a.ExtraGridlineStyle = s);

            if (gridlines.Ticks == null)
                return;

            axis.ExtraGridlines = gridlines.Ticks.ToArray();
        }

        private void ConfigureTicks(LinearAxis axis, AxisStepConfigurator ticks,
            Action<LinearAxis, OxyColor> gridColourSetter,
            Action<LinearAxis, double> gridThicknessSetter,
            Action<LinearAxis, LineStyle> gridStyleSetter,
            Action<LinearAxis, double> tickSizeSetter,
            Action<LinearAxis, double> tickStepSetter)
        {
            ConfigureLine(axis, ticks, gridColourSetter, gridThicknessSetter, gridStyleSetter);
            ConfiguratorHelper.SetIfNotNull(ticks.TickSize, s => tickSizeSetter(axis, s));
            ConfiguratorHelper.SetIfNotNull(ticks.Step, s => tickStepSetter(axis, s));
        }

        private void ConfigureLine(LinearAxis axis, LineConfigurator line,
            Action<LinearAxis, OxyColor> gridColourSetter,
            Action<LinearAxis, double> gridThicknessSetter, Action<LinearAxis, LineStyle> gridStyleSetter)
        {
            ConfiguratorHelper.SetIfNotNull(line.Colour, c => gridColourSetter(axis, c));
            ConfiguratorHelper.SetIfNotNull(line.Thickness, t => gridThicknessSetter(axis, t));
            ConfiguratorHelper.SetIfNotNull(line.Style, s => gridStyleSetter(axis, s));
        }

        private AxisPosition GetPosition()
            => Direction switch
            {
                AxisDirection.X => IsSecondary ? AxisPosition.Top : AxisPosition.Bottom,
                AxisDirection.Y => IsSecondary ? AxisPosition.Right : AxisPosition.Left,
                _ => throw new ArgumentOutOfRangeException(string.Empty, "Unknown direction")
            };
    }
}