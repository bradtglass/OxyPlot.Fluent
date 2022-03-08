using System.ComponentModel;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A configurator that can only exist in the <see cref="ConfigurationState.NotSet" /> or
    ///     <see cref="ConfigurationState.Include" /> states.
    /// </summary>
    public interface IBiStateConfigurator
    {
        /// <summary>
        ///     Sets the <see cref="Configurator.State" /> to <see cref="ConfigurationState.Include" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void ToIncludedState();

        /// <summary>
        ///     Sets the <see cref="Configurator.State" /> to <see cref="ConfigurationState.NotSet" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void ToNotSetState();
    }
}