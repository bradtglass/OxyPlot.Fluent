using System;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     A configurator that also supports building new configured instances of <typeparamref name="T" /> as
    ///     <typeparamref name="TBuild" /> which is the same as or a base type of <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type to configure.</typeparam>
    /// <typeparam name="TBuild">
    ///     The type to build as (can be less specific than <typeparamref name="T" /> to allow for
    ///     collections of generic configurators).``
    /// </typeparam>
    public abstract class BuildableConfigurator<T, TBuild> : Configurator<T>, IBuildable<TBuild> 
        where T : TBuild
    {
        /// <summary>
        ///     Activates and configures an <typeparamref name="TBuild" />.
        /// </summary>
        public TBuild Build()
        {
            T axis = Activate();
            Configure(axis);

            return axis;
        }

        /// <summary>
        ///     Creates a new instance of <typeparamref name="T" />.
        /// </summary>
        /// <remarks>
        ///     Must be overriden if <typeparamref name="T" /> does not have a default constructor.
        /// </remarks>
        protected virtual T Activate()
            => Activator.CreateInstance<T>();
    }
}