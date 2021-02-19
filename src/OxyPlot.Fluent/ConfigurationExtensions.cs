using System;
using OxyPlot.Fluent.Configurators;

namespace OxyPlot.Fluent
{
    internal static class ConfigurationExtensions
    {
        public static TConfigurator Set<TConfigurator, TProperty>(this TConfigurator configurator,
            Func<TConfigurator, ConfigurableProperty<TProperty>> propertyCallback, TProperty? value)
            where TConfigurator : IConfigurator
        {
            ConfigurableProperty<TProperty> property = propertyCallback(configurator);
            property.Set(value);

            return configurator;
        }

        public static TConfigurator Unset<TConfigurator, TProperty>(this TConfigurator configurator,
            Func<TConfigurator, ConfigurableProperty<TProperty>> propertyCallback)
            where TConfigurator : IConfigurator
        {
            ConfigurableProperty<TProperty> property = propertyCallback(configurator);
            property.Unset();

            return configurator;
        }

        public static TParent With<TParent, TChild>(this TParent configurator,
            Func<TParent, TChild> childCallback, Action<TChild>? configure)
            where TParent : IConfigurator
            where TChild : IBiStateConfigurator
        {
            TChild child = childCallback(configurator);
            child.ToIncludedState();

            configure?.Invoke(child);

            return configurator;
        }

        public static TParent Without<TParent, TChild>(this TParent configurator,
            Func<TParent, TChild> childCallback)
            where TParent : IConfigurator
            where TChild : ITriStateConfigurator
        {
            TChild child = childCallback(configurator);
            child.ToExcludedState();

            return configurator;
        }
    }
}