using System;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    // This replaced the use of nullable properties to allow for reference types to have a set/unset state.
    /// <summary>
    /// A configurator property that exists in a set or unset state.
    /// </summary>
    [PublicAPI]
    public class ConfigurableProperty<T>
    {
        /// <summary>
        /// Converts this property to it's value type.
        /// </summary>
        public static implicit operator T?(ConfigurableProperty<T?> property)
            => property.Value;

        /// <summary>
        /// Sets the <see cref="Value"/> and updates <see cref="IsSet"/>.
        /// </summary>
        public void Set(T? value)
        {
            Value = value;
            IsSet = true;
        }

        /// <summary>
        /// Updates <see cref="IsSet"/> to transition to an unset state.
        /// </summary>
        public void Unset()
        {
            IsSet = false;
        }

        /// <summary>
        /// Applies the <see cref="Value"/> if <see cref="IsSet"/> is <see langword="true"/>.
        /// </summary>
        /// <param name="owner">The owner of the property to apply a change to (this simplifies the syntax and allows the use of static delegates).</param>
        /// <param name="applicator">A callback to invoke to set the property on the owner, <paramref name="owner"/> is passed in as the first parameter, <see cref="Value"/> as the second.</param>
        public void ApplyIfSet<TOwner>(TOwner owner, Action<TOwner, T?> applicator)
        {
            if (IsSet)
                applicator(owner, Value);
        }

        /// <summary>
        /// The current value.
        /// </summary>
        public T? Value { get; private set; }
        
        /// <summary>
        /// A <see cref="bool"/> indicating if the value has been explicitly set.
        /// </summary>
        public bool IsSet { get; set; }
    }
}