# OxyPlot.Fluent
An unofficial extension library for OxyPlot to create plots using a fluent API.

## This library is still in development. For a flavour of what the aim is, take a look at the first test sample below
```csharp
[STAThread]
private static void Main(string[] args)
{
    Window window = Figure.Configure()
        .WithPlot()
        .WithTitle("Sample Plot")
        .WithLine(GetData())
        .Plot
        .Figure
        .Build()
        .AsWindow();

    window.ShowDialog();
}

private static IEnumerable<DataPoint> GetData()
{
    yield return new DataPoint(0, 0);
    yield return new DataPoint(4, 3);
    yield return new DataPoint(6, 6);
    yield return new DataPoint(7.4, 3);
    yield return new DataPoint(8, 2);
}
```
