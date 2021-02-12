using System;

namespace OxyPlot.Fluent.Configurators
{
    public static class ConfiguratorHelper
    {
        public static void SetIfNotNull<T>(T? value, Action<T> setter)
            where T : struct
        {
            if (value.HasValue)
                setter(value.Value);
        }
    }
}