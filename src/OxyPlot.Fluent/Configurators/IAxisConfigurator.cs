using System;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Interface for a configurator that can build and configure an <see cref="Axis" />.
    /// </summary>
    public interface IAxisConfigurator : IBuildable<Axis>, IConfigurator<Axis>
    {
        /// <summary>
        ///     The configuration for <see cref="Axis.Position" />.
        /// </summary>
        public AxisPositionConfigurator Position { get; }
        
        /// <summary>
        /// The concrete type of axis to be configured.
        /// </summary>
        public Type ConcreteAxisType { get; }
    }
}