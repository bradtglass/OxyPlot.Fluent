using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    ///     Configuration options for a <see cref="Figure" /> (one or more plots with a layout).
    /// </summary>
    [PublicAPI]
    public sealed class FigureConfigurator : Configurator
    {
        /// <inheritdoc />
        public FigureConfigurator()
        {
            State = ConfigurationState.Include;
        }
        
        /// <summary>
        ///     The value to set the overall title to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public  ConfigurableProperty<string> Title { get; } = new();

        /// <summary>
        ///     The value to set the window caption to.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public ConfigurableProperty<string> WindowTitle { get; } = new();

        /// <summary>
        ///     The number of rows in the plot grid.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Rows { get; set; }

        /// <summary>
        ///     The number of columns in the plot grid.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Columns { get; set; }

        /// <summary>
        ///     The configured plots to display in the figure grid.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public Dictionary<Cell, PlotConfigurator> Plots { get; } = new();

        /// <summary>
        ///     Creates and configures a <see cref="Figure" /> specified by the options in this <see cref="FigureConfigurator" />.
        /// </summary>
        public Figure Build()
        {
            if (State == ConfigurationState.NotSet || State == ConfigurationState.Exclude)
                throw new NotSupportedException($"Invalid configurator state: {State}");
            
            IEnumerable<Plot> plots = Plots.Select(p => new Plot(p.Value.Build(), p.Key));

            return new Figure(plots,
                Title.Value,
                Rows,
                Columns,
                WindowTitle.Value,
                FigureManager.Instance.Next());
        }
    }
}