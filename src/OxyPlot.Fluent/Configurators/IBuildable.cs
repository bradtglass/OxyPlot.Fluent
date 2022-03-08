using System.ComponentModel;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     An entity that can create and configure a <typeparamref name="TBuild" />.
    /// </summary>
    /// <typeparam name="TBuild">The type to build.</typeparam>
    [PublicAPI]
    public interface IBuildable<out TBuild>
    {
        /// <summary>
        ///     Activates and configures an <typeparamref name="TBuild" />.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        TBuild Build();
    }
}