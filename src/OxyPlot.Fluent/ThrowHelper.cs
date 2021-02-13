using System;
using System.Diagnostics.CodeAnalysis;

namespace OxyPlot.Fluent
{
    internal static class ThrowHelper
    {
        /// <summary>
        ///     Generates an exception and returns true if the parameter is less than zero.
        /// </summary>
        public static bool NegativeArgument(int value, string paramName, [NotNullWhen(true)] out Exception? exception)
        {
            if (value >= 0)
            {
                exception = null;

                return false;
            }

            exception = new ArgumentOutOfRangeException(paramName, value, "Value cannot be less than zero");

            return true;
        }
    }
}