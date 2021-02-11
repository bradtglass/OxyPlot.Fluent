using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    [PublicAPI]
    public class Figure
    {
        public FigureConfigurator Create()
            => new();
    }
}