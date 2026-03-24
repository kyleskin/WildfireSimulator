using Spectre.Console.Rendering;

namespace WildfireSimulator;

public sealed class Tree : IForestSquare
{
    private int _age;
    
    
    public (int x, int y) Position { get; init; }
    public ForestSquareState State { get; private set; }

    public Tree((int x, int y) position)
    {
        Position = position;
        State = ForestSquareState.Sapling;
        
        SpawnTree();
    }

    public void AgeYear()
    {
        _age++;
        
    }

    public void CatchFire()
    {
        var chanceOfCatchingFire = Random.Shared.Next(100);
        
        State = State switch
        {
            ForestSquareState.Sapling => chanceOfCatchingFire < 40 ? ForestSquareState.OnFire : ForestSquareState.Sapling,
            ForestSquareState.Juvenile => chanceOfCatchingFire < 20 ? ForestSquareState.OnFire : ForestSquareState.Juvenile,
            ForestSquareState.Mature => chanceOfCatchingFire < 10 ? ForestSquareState.OnFire : ForestSquareState.Mature,
            ForestSquareState.Diseased => chanceOfCatchingFire < 30 ? ForestSquareState.OnFire : ForestSquareState.Diseased,
            ForestSquareState.Rotten => ForestSquareState.OnFire,
            ForestSquareState.OnFire => ForestSquareState.OnFire,
            _ => throw new ArgumentOutOfRangeException(nameof(State), $"Invalid tree state: {State}")
        };
    }

    public Measurement Measure(RenderOptions options, int maxWidth)
    {
        return ForestSquareRenderer.Measure();
    }

    public IEnumerable<Segment> Render(RenderOptions options, int maxWidth)
    {
        return ForestSquareRenderer.Render(State, options);
    }

    private void SpawnTree()
    {
        _age = Random.Shared.Next(100);

        State = _age switch
        {
            <= 10 => ForestSquareState.Sapling,
            <= 30 => ForestSquareState.Juvenile,
            <= 80 => ForestSquareState.Mature,
            <= 95 => ForestSquareState.Diseased,
            _ => ForestSquareState.Rotten,
        };
    }
}