using System;
using System.ComponentModel;
using JetBrains.Annotations;
using OxyPlot.Axes;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for an <see cref="AxisPosition" />.
    /// </summary>
    [PublicAPI]
    public class AxisPositionConfigurator : Configurator, IBiStateConfigurator
    {
        /// <summary>
        ///     The direction of the axis to configure.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public AxisDirection Direction { get; set; }

        /// <summary>
        ///     A <see langword="bool" /> indicating if the axis is to be used as the secondary axis for a plot (
        ///     <see cref="AxisPosition.Right" /> or <see cref="AxisPosition.Top" />).
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsSecondary { get; set; }

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToIncludedState()
            => State = ConfigurationState.Include;

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ToNotSetState()
            => State = ConfigurationState.NotSet;

        /// <summary>
        ///     Configures an axis.
        /// </summary>
        /// <param name="target">The axis to configure.</param>
        /// <param name="setKey">
        ///     An optional bool indicating if the <see cref="Axis.Key" /> should also be set.
        ///     <para>
        ///         The <see cref="Axis.Key" /> is used internally to link a <see cref="Series.Series" /> to the
        ///         <see cref="Axis" />.
        ///     </para>
        /// </param>
        public void Configure(Axis target, bool setKey = false)
        {
            switch (State)
            {
                case ConfigurationState.NotSet:
                    return;
                case ConfigurationState.Include:
                    target.Position = CalculatePosition(Direction, IsSecondary);
                    target.Key = CalculateKey(Direction, IsSecondary);
                    return;
                case ConfigurationState.Exclude:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the position represented by this <see cref="AxisPositionConfigurator"/>.
        /// </summary>
        public AxisPosition GetPosition()
            => CalculatePosition(Direction, IsSecondary);
        
        /// <summary>
        ///     Gets the position from a direction and if the <see cref="Axis" /> is the secondary or primary.
        /// </summary>
        public static AxisPosition CalculatePosition(AxisDirection direction, bool isSecondary)
            => direction switch
            {
                AxisDirection.X => isSecondary ? AxisPosition.Top : AxisPosition.Bottom,
                AxisDirection.Y => isSecondary ? AxisPosition.Right : AxisPosition.Left,
                _ => throw new ArgumentOutOfRangeException(string.Empty, "Unknown direction")
            };

        /// <summary>
        ///     Gets the default key based on the axis position configuration properties.
        /// </summary>
        public static string CalculateKey(AxisDirection direction, bool isSecondary)
            => $"{direction}:{(isSecondary ? "Secondary" : "Primary")}";

        /// <summary>
        ///     Gets the default key based on the axis position.
        /// </summary>
        public static string CalculateKey(AxisPosition position)
            => position switch
            {
                AxisPosition.None => string.Empty,
                AxisPosition.Left => CalculateKey(AxisDirection.Y, false),
                AxisPosition.Right => CalculateKey(AxisDirection.Y, true),
                AxisPosition.Top => CalculateKey(AxisDirection.X, true),
                AxisPosition.Bottom => CalculateKey(AxisDirection.X, false),
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };
    }
}