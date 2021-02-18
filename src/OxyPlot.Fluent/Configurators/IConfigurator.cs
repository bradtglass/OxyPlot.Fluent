using System;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A type that configures properties.
    /// </summary>
    [PublicAPI]
    public interface IConfigurator : IFluentInterface
    {
        /// <summary>
        ///     The state of the configurator. Not all values are valid for all configurators.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Configurators for an element that must be visible or that can't control their own
        ///         visibility for can only exist in the <see cref="ConfigurationState.NotSet" /> or
        ///         <see cref="ConfigurationState.Include" /> state (e.g. <see cref="AxisTickConfigurator" />). See
        ///         <see cref="IBiStateConfigurator" />.
        ///     </para>
        ///     <para>
        ///         Configurators that can control their visibility can exist in any of the 3 states (e.g.
        ///         <see cref="LegendConfigurator" />). See <see cref="ITriStateConfigurator" />.
        ///     </para>
        /// </remarks>
        ConfigurationState State { get; }
    }

    /// <summary>
    ///     Primary configurator for a specific type <typeparamref name="T" />, for configurators that are generic or only
    ///     configure some properties of a type use <see cref="Configurator" />.
    /// </summary>
    /// <typeparam name="T">The type to configure.</typeparam>
    [PublicAPI]
    public interface IConfigurator<in T> : IConfigurator
    {
        /// <summary>
        ///     Configures the <paramref name="target" />.
        /// </summary>
        /// <param name="target">The instance to configure.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="target" /> is <see langword="null" />.</exception>
        void Configure([NotNull] T target);
    }
}