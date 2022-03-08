using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    /// <summary>
    /// Extensions methods for working with configurators.
    /// </summary>
    [PublicAPI]
    public static class ConfiguratorExtensions
    {
        /// <summary>
        /// Sets a <see cref="ConfigurableProperty{T}"/> on a <typeparamref name="TConfigurator"/>.
        /// </summary>
        /// <param name="configurator">The configurator that owns the property.</param>
        /// <param name="propertyCallback">A callback to select the property on the <paramref name="configurator"/>.</param>
        /// <param name="value">The value to set the <typeparamref name="TProperty"/> to.</param>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TConfigurator Set<TConfigurator, TProperty>(this TConfigurator configurator,
            Func<TConfigurator, ConfigurableProperty<TProperty>> propertyCallback, TProperty? value)
            where TConfigurator : IConfigurator
        {
            ConfigurableProperty<TProperty> property = propertyCallback(configurator);
            property.Set(value);

            return configurator;
        }

        /// <summary>
        /// Unsets a <see cref="ConfigurableProperty{T}"/> on a <typeparamref name="TConfigurator"/>.
        /// </summary>
        /// <param name="configurator">The configurator that owns the property.</param>
        /// <param name="propertyCallback">A callback to select the property on the <paramref name="configurator"/>.</param>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TConfigurator Unset<TConfigurator, TProperty>(this TConfigurator configurator,
            Func<TConfigurator, ConfigurableProperty<TProperty>> propertyCallback)
            where TConfigurator : IConfigurator
        {
            ConfigurableProperty<TProperty> property = propertyCallback(configurator);
            property.Unset();

            return configurator;
        }

        /// <summary>
        /// Sets a child <see cref="IConfigurator"/> to <see cref="ConfigurationState.Include"/> state and optionally configure it.
        /// </summary>
        /// <param name="configurator">The parent configurator.</param>
        /// <param name="childCallback">A callback to select the child configurator.</param>
        /// <param name="configure">[optional] A callback that should be invoked to configure the child configurator.</param>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TParent With<TParent, TChild>(this TParent configurator,
            Func<TParent, TChild> childCallback, Action<TChild>? configure = null)
            where TParent : IConfigurator
            where TChild : IBiStateConfigurator
        {
            TChild child = childCallback(configurator);
            child.ToIncludedState();

            configure?.Invoke(child);

            return configurator;
        }

        /// <summary>
        /// Sets a child <see cref="IConfigurator"/> to <see cref="ConfigurationState.Exclude"/> state.
        /// </summary>
        /// <param name="configurator">The parent configurator.</param>
        /// <param name="childCallback">A callback to select the child configurator.</param>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TParent Without<TParent, TChild>(this TParent configurator,
            Func<TParent, TChild> childCallback)
            where TParent : IConfigurator
            where TChild : ITriStateConfigurator
        {
            TChild child = childCallback(configurator);
            child.ToExcludedState();

            return configurator;
        }
        
        /// <summary>
        ///     Sets a property in the <see cref="Configurator{T}.AdditionalProperties"/>. 
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        /// <param name="propertyExpression">An expression to select the property on the target type.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <typeparam name="TConfigurator">The configurator type that owns the additional properties.</typeparam>
        /// <typeparam name="TConfigures">The type that the <paramref name="configurator"/> configures.</typeparam>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TConfigurator SetAdditional<TConfigurator, TConfigures, TProperty>(
            this TConfigurator configurator,
            Expression<Func<TConfigures, TProperty?>> propertyExpression,
            TProperty? value)
            where TConfigurator : Configurator<TConfigures>
        {
            configurator.AdditionalProperties.Set(propertyExpression, value);

            return configurator;
        }
        
        /// <summary>
        ///     Sets a property in the <see cref="Configurator{T}.AdditionalProperties"/> by name. 
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="setter">The callback that will be invoked to set the value on the target.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <typeparam name="TConfigurator">The configurator type that owns the additional properties.</typeparam>
        /// <typeparam name="TConfigures">The type that the <paramref name="configurator"/> configures.</typeparam>
        /// <returns>The <paramref name="configurator"/>.</returns>
        public static TConfigurator SetAdditional<TConfigurator, TConfigures, TProperty>(
            this TConfigurator configurator,
            string propertyName,
            Action<TConfigures, TProperty?> setter,
            TProperty? value)
            where TConfigurator : Configurator<TConfigures>
        {
            configurator.AdditionalProperties.Set(propertyName, setter, value);

            return configurator;
        }
    }
}