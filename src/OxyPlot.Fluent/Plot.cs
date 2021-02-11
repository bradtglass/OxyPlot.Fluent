using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public static class Plot
    {
        public static PlotConfigurator Create()
            => new(null);
    }
}