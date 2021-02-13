using System;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
/// <summary>
/// Configuration options for an <see cref="Axis"/>.
/// </summary>
    [PublicAPI]
    public sealed class AxisConfigurator : IFluentInterface
    {
        /// <summary>
        /// Instantiates a new <see cref="AxisConfigurator"/>.
        /// </summary>
        public AxisConfigurator(AxisDirection direction, bool isSecondary)
        {
            Direction = direction;
            IsSecondary = isSecondary;
        }

        /// <summary>
        /// The direction of the axis to configure.
        /// </summary>
        public AxisDirection Direction { get; }

        /// <summary>
        /// A <see langword="bool"/> indicating if the axis is to be used as the secondary axis for a plot (<see cref="AxisPosition.Right"/> or <see cref="AxisPosition.Top"/>).
        /// </summary>
        public bool IsSecondary { get; }

        /// <summary>
        /// The value to set <see cref="Axis.Minimum"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public double? Minimum { get; set; }

        /// <summary>
        /// The value to set <see cref="Axis.Maximum"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public double? Maximum { get; set; }

        /// <summary>
        /// The value to set <see cref="Axis.Angle"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public double? LabelAngle { get; set; }

        /// <summary>
        /// The value to set <see cref="Axis.StringFormat"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public string? LabelFormat { get; set; }
        
        /// <summary>
        /// The value to set <see cref="Axis.Title"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// The configuration for the MajorTicks or <see langword="null"/> to skip configuration for it.
        /// </summary>
        public AxisTickConfigurator? MajorTicks { get; set; }

        /// <summary>
        /// The configuration for the MinorTicks or <see langword="null"/> to skip configuration for it.
        /// </summary>
        public AxisTickConfigurator? MinorTicks { get; set; }

        /// <summary>
        /// The configuration for the <see cref="Axis.ExtraGridlines"/> or <see langword="null"/> to skip configuration for it.
        /// </summary>
        public CustomGridlinesConfigurator? CustomGridlines { get; set; }

        /// <summary>
        /// The value to set <see cref="Axis.TickStyle"/> to or <see langword="null"/> to skip configuring this property.
        /// </summary>
        public TickStyle? TickStyle { get; set; }

        /// <summary>
        /// Creates and configures an <see cref="Axis"/> specified by the options in this <see cref="AxisConfigurator"/>.
        /// </summary>
        public Axis Build()
        {
            LinearAxis axis = new()
            {
                Position = GetPosition(),
                Title = Title,
                Key = $"{Direction}:{(IsSecondary ? "Secondary" : "Primary")}",
                StringFormat = LabelFormat
            };

            ConfiguratorHelper.SetIfNotNull(Minimum, m => axis.Minimum = m);
            ConfiguratorHelper.SetIfNotNull(Maximum, m => axis.Maximum = m);
            ConfiguratorHelper.SetIfNotNull(LabelAngle, a => axis.Angle = a);
            ConfiguratorHelper.SetIfNotNull(TickStyle, s => axis.TickStyle = s);

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

            axis.ExtraGridlines = gridlines.Ticks;
        }

        private void ConfigureTicks(LinearAxis axis, AxisTickConfigurator ticks,
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