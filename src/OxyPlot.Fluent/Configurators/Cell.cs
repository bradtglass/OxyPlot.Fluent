#pragma warning disable 1572 Invalid warning for XML docs for positional properties (https://github.com/dotnet/roslyn/issues/44571)
namespace OxyPlot.Fluent.Configurators
{
    /// <summary>
    /// A 2D position in a unit grid.
    /// </summary>
    /// <param name="X">The column index.</param>
    /// <param name="Y">The row index.</param>
    public record Cell(int X, int Y);
}