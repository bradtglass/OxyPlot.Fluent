using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A collection of properties that can be set on a target type.
    /// </summary>
    [PublicAPI]
    public class ConfigurableProperties<T>
    {
        private readonly Dictionary<string, Property> properties = new();

        /// <summary>
        ///     Sets a property.
        /// </summary>
        /// <param name="propertyExpression">An expression to select the property on the target type.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <returns>The inner <see cref="ConfigurableProperty{T}" /> used by the collection.</returns>
        public ConfigurableProperty<TProperty> Set<TProperty>(Expression<Func<T, TProperty?>> propertyExpression,
            TProperty? value)
        {
            PropertyInfo info = GetProperty(propertyExpression);

            string name = GetPropertyName(info);
            Action<T, TProperty?> setter = GetSetter<TProperty>(info);

            return Set(name, setter, value);
        }

        /// <summary>
        ///     Sets a property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="setter">The callback that will be invoked to set the value on the target.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <returns>The inner <see cref="ConfigurableProperty{T}" /> used by the collection.</returns>
        public ConfigurableProperty<TProperty> Set<TProperty>(string propertyName, Action<T, TProperty?> setter,
            TProperty? value)
        {
            ConfigurableProperty<TProperty> settableProperty = GetOrAdd(propertyName, setter);
            settableProperty.Set(value);

            return settableProperty;
        }

        /// <summary>
        ///     Gets a property from the collection.
        /// </summary>
        /// <param name="propertyExpression">An expression to select the property on the target type.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <returns>The inner <see cref="ConfigurableProperty{T}" /> used by the collection or null if the property doesn't exist in the collection.</returns>
        public ConfigurableProperty<TProperty>? Get<TProperty>(Expression<Func<T, TProperty?>> propertyExpression)
            => Get<TProperty>(GetPropertyName(GetProperty(propertyExpression)));

        
        /// <summary>
        ///     Gets a property from the collection.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <returns>The inner <see cref="ConfigurableProperty{T}" /> used by the collection or null if the property doesn't exist in the collection.</returns>
        public ConfigurableProperty<TProperty>? Get<TProperty>(string propertyName)
        {
            if (properties.TryGetValue(propertyName, out Property property))
                return (ConfigurableProperty<TProperty>?) property.InnerUntypedProperty;

            return null;
        }
        
        private ConfigurableProperty<TProperty> GetOrAdd<TProperty>(string propertyName, Action<T, TProperty?> setter)
        {
            if (properties.TryGetValue(propertyName, out Property untypedProperty))
            {
                if (untypedProperty is Property<TProperty> property)
                    return property.InnerTypedProperty;

                throw new ArgumentException("The existing property in the collection is for a different property type",
                    nameof(propertyName));
            }

            Property<TProperty> newProperty = new(setter);
            properties.Add(propertyName, newProperty);

            return newProperty.InnerTypedProperty;
        }

        /// <summary>
        ///     Unsets the property value.
        /// </summary>
        /// <param name="propertyExpression">An expression to select the property on the target type.</param>
        /// <typeparam name="TProperty">The property type.</typeparam>
        public void Unset<TProperty>(Expression<Func<T, TProperty?>> propertyExpression)
        {
            PropertyInfo info = GetProperty(propertyExpression);
            string name = GetPropertyName(info);

            Unset(name);
        }

        /// <summary>
        ///     Unsets the property value.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        public void Unset(string propertyName)
        {
            if (properties.TryGetValue(propertyName, out Property property))
                property.Unset();
        }

        /// <summary>
        ///     Applies the set properties in this collection to the <paramref name="target" />.
        /// </summary>
        public void Apply(T target)
        {
            foreach (Property property in properties.Values) property.ApplyIfSet(target);
        }

        private static PropertyInfo GetProperty<TProperty>(Expression<Func<T, TProperty?>> propertyExpression)
        {
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression ??
                                                throw new ArgumentException(
                                                    "Expression is not valid expression for a property on the target type",
                                                    nameof(propertyExpression));

            PropertyInfo info = memberExpression.Member as PropertyInfo ??
                                throw new ArgumentException(
                                    $"Expression targets an invalid member: {memberExpression.Member.GetType().Name}",
                                    nameof(propertyExpression));

            return info;
        }

        private static Action<T, TProperty?> GetSetter<TProperty>(PropertyInfo info)
        {
            if (info.SetMethod == null)
                throw new ArgumentException(
                    $"Property {info.Name} on {info.DeclaringType?.FullName} does not have an accessible setter",
                    nameof(info));

            return (target, value) => info.SetValue(target, value);
        }

        private string GetPropertyName(PropertyInfo info)
            => info.Name;

        private abstract class Property
        {
            public abstract void ApplyIfSet(T target);

            public abstract void Unset();
            
            public abstract object InnerUntypedProperty { get; }
        }

        private class Property<TProperty> : Property
        {
            private readonly Action<T, TProperty?> setter;

            public Property(Action<T, TProperty?> setter)
            {
                this.setter = setter;
            }

            public ConfigurableProperty<TProperty> InnerTypedProperty { get; } = new();

            public override void ApplyIfSet(T target)
                => InnerTypedProperty.ApplyIfSet(target, setter);

            public override void Unset()
                => InnerTypedProperty.Unset();

            public override object InnerUntypedProperty => InnerTypedProperty;
        }
    }
}