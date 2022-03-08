using System;
using System.ComponentModel;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A type that configures properties.
    /// </summary>
    [PublicAPI]
    public abstract class Configurator : IConfigurator
    {
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurationState State { get; protected set; }

        /// <inheritdoc />
        /// <remarks>
        /// Must be overridden in a derived type if their is not a default constructor.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual IConfigurator CreateNewInstance()
            => (IConfigurator) Activator.CreateInstance(GetType());
    }

    /// <summary>
    ///     Primary configurator for a specific type <typeparamref name="T" />, for configurators that are generic or only
    ///     configure some properties of a type use <see cref="Configurator" />.
    /// </summary>
    /// <typeparam name="T">The type to configure.</typeparam>
    [PublicAPI]
    public abstract class Configurator<T> : Configurator, IConfigurator<T>
    {
        /// <summary>
        ///     Additional properties to set on the target.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperties<T> AdditionalProperties { get; } = new();

        /// <inheritdoc />
        public virtual void Configure(T target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            ConfigureImplementedProperties(target);
            AdditionalProperties.Apply(target);
        }

        /// <summary>
        ///     Configures the <paramref name="target" /> with any additional properties implemented on the inheriting type.
        /// </summary>
        protected abstract void ConfigureImplementedProperties(T target);
    }
}