using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    /// Combines properties and child configurations of a <see cref="Configurator"/> instance with those of a secondary instance.
    /// </summary>
    [PublicAPI]
    public class Combinator<TPrimary, TSecondary>
        where TPrimary : IConfigurator, TSecondary
    {
        private readonly TPrimary primary;
        private readonly TSecondary[] secondaries;

        /// <summary>
        /// Creates a new <see cref="Combinator{TPrimary,TSecondary}"/>.
        /// </summary>
        /// <param name="primary">The primary instance, values set on this instance take priority over those on the <paramref name="secondaries"/>.</param>
        /// <param name="secondaries">The instances to apply on top of <paramref name="primary"/>.</param>
        public Combinator(TPrimary primary, params TSecondary[] secondaries)
        {
            this.primary = primary;
            this.secondaries = secondaries;
        }

        /// <summary>
        /// Run the combinator and create a new combined instance.
        /// </summary>
        /// <returns></returns>
        public TPrimary Combine()
        {
            TPrimary combined = (TPrimary) primary.CreateNewInstance();

            IReadOnlyList<object> from = new[] {primary}
                .Cast<object>()
                .Concat(secondaries.Cast<object>())
                .ToList();
            
            CombineProperties(combined, from);
            CombineChildConfigurators(combined, from);

            return combined;
        }

        private static void CombineChildConfigurators(object target, IReadOnlyList<object> from)
        {
            List<List<PropertyInfo>> fromProperties = from
                .Select(f => f.GetType())
                .Select(t => GetChildConfigurators(t).ToList())
                .ToList();

            foreach (PropertyInfo childConfiguratorInfo in GetChildConfigurators(target.GetType()))
            {
                IConfigurator childConfigurator = (IConfigurator) childConfiguratorInfo.GetValue(target);

                if (childConfigurator.State == ConfigurationState.Exclude)
                    continue;

                IReadOnlyList<IConfigurator> combineWith = fromProperties
                    .Select(l => l.FirstOrDefault(p => p.Name == childConfiguratorInfo.Name))
                    .Select((p, i) => p?.GetValue(from[i]))
                    .Where(c => c != null)
                    .Cast<IConfigurator>()
                    .ToList();

                using (IEnumerator<IConfigurator> enumerator = combineWith.GetEnumerator())
                {
                    while (childConfigurator.State == ConfigurationState.NotSet && enumerator.MoveNext())
                    {
                        ConfigurationState currentState = enumerator.Current!.State;
                        switch (currentState)
                        {
                            case ConfigurationState.NotSet:
                                break;
                            case ConfigurationState.Include:
                                if (childConfigurator is not IBiStateConfigurator biStateConfigurator)
                                    throw new InvalidOperationException(
                                        $"Failed to combine children because the child cannot be set to an included state but one of the children to be combined with is in an included state ({childConfigurator.GetType().FullName} must implement the {nameof(IBiStateConfigurator)})");

                                biStateConfigurator.ToIncludedState();
                                break;
                            case ConfigurationState.Exclude:
                                if (childConfigurator is not ITriStateConfigurator triStateConfigurator)
                                    throw new InvalidOperationException(
                                        $"Failed to combine children because the child cannot be set to an excluded state but one of the children to be combined with is in an excluded state ({childConfigurator.GetType().FullName} must implement the {nameof(ITriStateConfigurator)})");

                                triStateConfigurator.ToExcludedState();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("",
                                    $"The child configurator state was not known: {currentState}");
                        }
                    }
                }

                if (childConfigurator.State == ConfigurationState.Exclude)
                    continue;

                CombineProperties(childConfigurator, combineWith);
                CombineChildConfigurators(childConfigurator, combineWith);
            }   
        }

        private static void CombineProperties(object target, IReadOnlyList<object> from)
        {
            List<List<PropertyInfo>> fromProperties = from
                .Select(f=>f.GetType())
                .Select(f => GetConfigurableProperties(f).ToList())
                .ToList();

            foreach (PropertyInfo targetProperty in GetConfigurableProperties(target.GetType()))
            {
                int i = 0;
                bool propertySet = false;
                while (i < from.Count && !propertySet)
                {
                    if (fromProperties[i].FirstOrDefault(p => p.Name == targetProperty.Name) is { } fromProperty)
                        propertySet = CopyConfigurableProperty(fromProperty, from[i], targetProperty, target);

                    i++;
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetChildConfigurators(Type type)
        {
            Type configurator = typeof(IConfigurator);

            return type.GetProperties()
                .Where(p=>configurator.IsAssignableFrom(p.PropertyType));
        }

        private static IEnumerable<PropertyInfo> GetConfigurableProperties(Type type)
        {
            Type configurableProperty = typeof(ConfigurableProperty<>);

            return type.GetProperties()
                /*ConfigurableProperty is sealed so this check is okay, if not we would need to check if it was a derived type as well*/
                .Where(p => p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.GetGenericTypeDefinition() == configurableProperty);
        }

        /// <summary>
        /// Copies the value from one <see cref="ConfigurableProperty{T}"/> to another if the <paramref name="source"/> is set.
        /// </summary>
        /// <returns><see langword="true"/> if the target property was set, otherwise <see langword="false"/>.</returns>
        private static bool CopyConfigurableProperty(PropertyInfo sourceInfo, object source,PropertyInfo targetInfo, object target)
        {
            object sourceProperty = sourceInfo.GetValue(source);
            
            if(!IsConfigurablePropertySet(sourceProperty))
                return false;

            object targetProperty = targetInfo.GetValue(target);
            object sourceValue = GetConfigurablePropertyValue(sourceProperty);
            
            SetConfigurableProperty(targetProperty, sourceValue);

            return true;
        }

        private static object GetConfigurablePropertyValue(object property)
        {
            PropertyInfo valueProperty = property.GetType()
                                             .GetProperty(nameof(ConfigurableProperty<object>.Value), 
                                                 BindingFlags.Public|BindingFlags.Instance)??
                                         throw new ArgumentException("Cannot find valid Value property", nameof(property));

            return valueProperty.GetValue(property);
        }

        private static void SetConfigurableProperty(object property, object newValue)
        {
            MethodInfo setMethod = property.GetType()
                .GetMethods(BindingFlags.Public|BindingFlags.Instance)
                .Where(m=>m.Name==nameof(ConfigurableProperty<object>.Set))
                .FirstOrDefault(m=>m.GetParameters().Length==1) ??
                                   throw new ArgumentException("Cannot find valid Set method", nameof(property));

            setMethod.Invoke(property, new[] {newValue});
        }

        private static bool IsConfigurablePropertySet(object property)
        {
            PropertyInfo isSetProperty = property.GetType()
                                           .GetProperty(nameof(ConfigurableProperty<object>.IsSet), 
                                               BindingFlags.Public|BindingFlags.Instance)??
                                       throw new ArgumentException("Cannot find valid IsSet property", nameof(property));

            return (bool) isSetProperty.GetValue(property);
        }
    }
}