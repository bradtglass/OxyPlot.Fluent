using System;
using System.Collections.Generic;
using System.Linq;

namespace OxyPlot.Fluent
{
    /// <summary>
    ///     Manages the lifetime of figures.
    /// </summary>
    internal class FigureManager
    {
        /// <summary>
        ///     The single instance of <see cref="FigureManager" />.
        /// </summary>
        public static FigureManager Instance = new();

        private readonly List<bool> numberStore = new(Enumerable.Repeat(false, 100));

        private FigureManager() { }

        /// <summary>
        ///     Create a new lifetime using the next available figure number.
        /// </summary>
        public Lifetime Next()
        {
            lock (numberStore)
            {
                int index = 0;
                while (true)
                {
                    if (numberStore.Count <= index)
                    {
                        numberStore.Add(true);
                        return new Lifetime(index + 1);
                    }

                    if (!numberStore[index])
                    {
                        numberStore[index] = true;
                        return new Lifetime(index + 1);
                    }

                    index++;
                }
            }
        }

        private void Release(Lifetime lifetime)
        {
            lock (numberStore)
            {
                numberStore[lifetime.Number - 1] = false;
            }
        }

        /// <summary>
        ///     A unique figure number with a lifetime, should be disposed when the figure is closed and cannot be reopened.
        /// </summary>
        public sealed class Lifetime : IDisposable
        {
            /// <summary>
            ///     Creates a new figure lifetime.
            /// </summary>
            public Lifetime(int number)
            {
                Number = number;
            }

            /// <summary>
            ///     The unique figure number (only one visible figure can obtain the same number).
            /// </summary>
            public int Number { get; }

            /// <summary>
            ///     The default window title.
            /// </summary>
            public string DefaultTitle => "Figure " + Number;

            /// <inheritdoc />
            public void Dispose()
            {
                ReleaseUnmanagedResources();
                GC.SuppressFinalize(this);
            }

            private void ReleaseUnmanagedResources()
            {
                Instance.Release(this);
            }

            ~Lifetime()
            {
                ReleaseUnmanagedResources();
            }
        }
    }
}