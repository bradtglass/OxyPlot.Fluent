using System;
using System.Linq.Expressions;
using OxyPlot.Fluent.Configurators;
using OxyPlot.Series;
using Xunit;

namespace OxyPlot.Fluent.Tests
{
    public class CombinatorTests
    {
        [Fact]
        public void CombinatorCreatesNewInstance()
        {
            LegendConfigurator a = new();
            LegendConfigurator b = new();

            Combinator<LegendConfigurator> combinator = new(a, b);

            LegendConfigurator combined = combinator.Combine();

            Assert.False(ReferenceEquals(a, combined));
            Assert.False(ReferenceEquals(b, combined));
        }

        [Fact]
        public void CombinatorCombinesAdditionalProperties()
        {
            LineSeriesConfigurator a = new();
            LineSeriesConfigurator b = new();
            
            Expression<Func<LineSeries, string?>> propertyExpression = ls=>ls.LabelFormatString;
            const string format = "ABC";

            a.SetAdditional(propertyExpression, format);

            // Property only set on a
            Combinator<LineSeriesConfigurator> combinator = new(a, b);

            LineSeriesConfigurator combined = combinator.Combine();

            ConfigurableProperty<string>? combinedProperty = combined.AdditionalProperties.Get(propertyExpression);
            Assert.NotNull(combinedProperty);
            Assert.True(combinedProperty!.IsSet);
            Assert.Equal(format, combinedProperty.Value);
            
            // Property set on a AND b, should take the value from a and ignore b
            b.SetAdditional(propertyExpression, "ANOTHER RANDOM VALUE");

            combinator = new Combinator<LineSeriesConfigurator>(a, b);

            combined = combinator.Combine();

            combinedProperty = combined.AdditionalProperties.Get(propertyExpression);
            Assert.NotNull(combinedProperty);
            Assert.True(combinedProperty!.IsSet);
            Assert.Equal(format, combinedProperty.Value);
        }

        [Fact]
        public void CombinatorAddsDeclaredProperties()
        {
            LegendConfigurator a = new();
            LegendConfigurator b = new();

            const LegendPlacement placement = LegendPlacement.Inside;
            const LegendPosition position = LegendPosition.LeftTop;
            b.Placement.Set(placement);
            a.Position.Set(position);

            Combinator<LegendConfigurator> combinator = new(a, b);
            LegendConfigurator combined = combinator.Combine();
            
            Assert.True(combined.Placement.IsSet);
            Assert.True(combined.Position.IsSet);
            Assert.Equal(placement, combined.Placement.Value);
            Assert.Equal(position, combined.Position.Value);
        }

        [Fact]
        public void CombinatorCombinesChildConfigurators()
        {
            LineSeriesConfigurator a = new();
            LineSeriesConfigurator b = new();

            OxyColor lineColour = OxyColors.Brown;
            const MarkerType markerType = MarkerType.Star;
            a.Line.Color.Set(lineColour);
            b.Marker.Type.Set(markerType);

            Combinator<LineSeriesConfigurator> combinator = new(a, b);
            LineSeriesConfigurator combined = combinator.Combine();
            
            Assert.True(combined.Line.Color.IsSet);
            Assert.True(combined.Marker.Type.IsSet);
            Assert.Equal(lineColour, combined.Line.Color.Value);
            Assert.Equal(markerType, combined.Marker.Type.Value);
        }

        [Fact]
        public void CombinatorCanAddLessDerivedTypes()
        {
            LineSeriesConfigurator a = new();
            XyAxisSeriesConfigurator b = new();

            OxyColor lineColour = OxyColors.Brown;
            const bool secondaryX = true;
            a.Line.Color.Set(lineColour);
            b.UseSecondaryXAxis.Set(secondaryX);

            Combinator<LineSeriesConfigurator> combinator = new(a, b);
            LineSeriesConfigurator combined = combinator.Combine();
            
            Assert.True(combined.Line.Color.IsSet);
            Assert.True(combined.UseSecondaryXAxis.IsSet);
            Assert.Equal(lineColour, combined.Line.Color.Value);
            Assert.Equal(secondaryX, combined.UseSecondaryXAxis.Value);
        }

        [Fact]
        public void CombinatorDoesNotOverrideSetProperties()
        {
            LegendConfigurator a = new();
            LegendConfigurator b = new();

            const LegendPlacement placement = LegendPlacement.Inside;
            const LegendPlacement ignoredPlacement = LegendPlacement.Inside;
            b.Placement.Set(placement);
            a.Placement.Set(ignoredPlacement);

            Combinator<LegendConfigurator> combinator = new(a, b);
            LegendConfigurator combined = combinator.Combine();
            
            Assert.True(combined.Placement.IsSet);
            Assert.Equal(placement, combined.Placement.Value);
        }
    }
}