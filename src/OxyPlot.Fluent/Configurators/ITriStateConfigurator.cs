using System.ComponentModel;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A configurator that can exist in any of the 3 <see cref="ConfigurationState" />.
    /// </summary>
    public interface ITriStateConfigurator : IBiStateConfigurator
    {
        /// <summary>
        ///     Sets the <see cref="Configurator.State" /> to <see cref="ConfigurationState.Exclude" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void ToExcludedState();
    }
}