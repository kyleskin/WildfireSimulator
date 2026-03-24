using Spectre.Console.Rendering;

namespace WildfireSimulator;

public sealed class ForestFloor : IForestSquare
{
    public (int x, int y) Position { get; init; }
    public ForestSquareState State { get; private set; }

    public ForestFloor((int x, int y) position)
    {
        Position = position;
    }

    public void AgeYear()
    {
    }

    public void CatchFire()
    {
    }

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ForestSquareRenderer.Measure();
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ForestSquareRenderer.Render(State, options);
    }
    
    public override string ToString()
    {
        return $"Forest Floor - x:{Position.x} y:{Position.y} - {State}";
    }
}