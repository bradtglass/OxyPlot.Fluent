using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Interface for a configurator that can build and configure an <see cref="Series.Series" />.
    /// </summary>
    public interface ISeriesConfigurator : IBuildable<Series.Series>, IConfigurator<Series.Series>
    {
        /// <summary>
        /// Gets the position of this series X axis or <see langword="null"/> if not an <see cref="Series.XYAxisSeries"/>.
        /// </summary>
        public AxisPosition? GetXPosition();
        
        /// <summary>
        /// Gets the position of this series X axis or <see langword="null"/> if not an <see cref="Series.XYAxisSeries"/>.
        /// </summary>
        public AxisPosition? GetYPosition();
    }
}