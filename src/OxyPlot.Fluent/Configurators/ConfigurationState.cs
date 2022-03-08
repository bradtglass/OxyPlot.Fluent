namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     The tri-state that a configurator can exist in.
    /// </summary>
    public enum ConfigurationState
    {
        /// <summary>
        ///     State has not been set explicitly, default value.
        /// </summary>
        NotSet,

        /// <summary>
        ///     The configurator should configure the target with any set properties and invoke any child configurators.
        /// </summary>
        Include,

        /// <summary>
        ///     The configurator should remove/hide the target from the plot. This value is not valid for all configurator
        ///     types because not all configurators can control their own visibility or don't represent a specific element
        ///     (e.g. <see cref="AxisPositionConfigurator" />).
        /// </summary>
        Exclude
    }
}