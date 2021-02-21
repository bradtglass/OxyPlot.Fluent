using System.Collections.Generic;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent.Tests
{
    [UsedImplicitly]
    public class ConfiguratorsFixture
    {
        public IReadOnlyList<IConfigurator> AllConfigurators { get; } = new List<IConfigurator>
        {
            new FigureConfigurator(),
            new PlotConfigurator(),
            new LineSeriesConfigurator(),
            new LegendConfigurator(),
            new LineConfigurator(),
            new MarkerConfigurator(),
            new AxisPositionConfigurator(),
            new AxisTickConfigurator(),
            new ExtraGridlinesConfigurator(),
            new AxisConfigurator<LinearAxis>()
        };
    }
}