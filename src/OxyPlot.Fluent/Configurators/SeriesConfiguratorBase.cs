using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Inheritable configuration options for a <see cref="Series" />.
    /// </summary>
    [PublicAPI]
    public abstract class SeriesConfiguratorBase<T> : BuildableConfigurator<T, Series.Series>, ISeriesConfigurator
        where T : Series.Series
    {
        /// <inheritdoc />
        protected SeriesConfiguratorBase()
        {
            State = ConfigurationState.Include;
        }

        /// <summary>
        ///     The value to set <see cref="Series.Title" /> to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<string> Title { get; } = new();


        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Configure(Series.Series target)
            => base.Configure((T) target);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract AxisPosition? GetXPosition();

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract AxisPosition? GetYPosition();

        /// <summary>
        ///     Configures <see cref="Series" /> properties based on the options in this <see cref="SeriesConfiguratorBase{T}" />.
        /// </summary>
        protected void ConfigureSeries(Series.Series target)
        {
            if (State == ConfigurationState.NotSet)
                return;

            Title.ApplyIfSet(target, (s, t) => s.Title = t);
        }
    }
}