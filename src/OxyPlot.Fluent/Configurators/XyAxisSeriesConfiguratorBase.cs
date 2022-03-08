using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Inheritable configuration options for a <see cref="XYAxisSeries" />.
    /// </summary>
    [PublicAPI]
    public abstract class XyAxisSeriesConfiguratorBase<T> : SeriesConfiguratorBase<T>
        where T : XYAxisSeries
    {
        /// <summary>
        ///     A <see langword="bool" /> indicating if this <see cref="Series" /> should be plotted against a secondary Y
        ///     axis.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<bool> UseSecondaryYAxis { get; } = new();

        /// <summary>
        ///     A <see langword="bool" /> indicating if this <see cref="Series" /> should be plotted against a secondary X
        ///     axis.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<bool> UseSecondaryXAxis { get; } = new();

        /// <summary>
        ///     Configures <see cref="XYAxisSeries" /> properties based on the options in this
        ///     <see cref="XyAxisSeriesConfiguratorBase{T}" />.
        /// </summary>
        protected void ConfigureXyAxisSeries(XYAxisSeries target)
        {
            ConfigureSeries(target);

            if (UseSecondaryXAxis.IsSet)
                target.XAxisKey = AxisPositionConfigurator.CalculateKey(AxisDirection.X, UseSecondaryXAxis);

            if (UseSecondaryYAxis.IsSet)
                target.YAxisKey = AxisPositionConfigurator.CalculateKey(AxisDirection.Y, UseSecondaryYAxis);
        }

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AxisPosition? GetXPosition()
            => AxisPositionConfigurator.CalculatePosition(AxisDirection.X, UseSecondaryXAxis);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AxisPosition? GetYPosition()
            => AxisPositionConfigurator.CalculatePosition(AxisDirection.Y, UseSecondaryYAxis);
    }
}