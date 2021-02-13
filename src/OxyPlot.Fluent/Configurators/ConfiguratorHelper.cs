using System;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Helper methods for use with configuration.
    /// </summary>
    public static class ConfiguratorHelper
    {
        /// <summary>
        ///     Invokes the <paramref name="setter" /> if the <paramref name="value" /> is not <see langword="null" />.
        /// </summary>
        public static void SetIfNotNull<T>(T? value, Action<T> setter)
            where T : struct
        {
            if (value.HasValue)
                setter(value.Value);
        }
    }
}