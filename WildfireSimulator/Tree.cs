using System.ComponentModel;
using Spectre.Console.Rendering;

namespace WildfireSimulator;

public sealed class Tree : IForestSquare
{
    private int _age;
    private int _timeAsState;
    
    
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
        _timeAsState++;
    }

    public void CatchFire()
    {
        var chanceOfCatchingFire = Random.Shared.Next(100);

        switch (State)
        {
            case ForestSquareState.Sapling when chanceOfCatchingFire < 40:
            case ForestSquareState.Juvenile when chanceOfCatchingFire < 20:
            case ForestSquareState.Mature when chanceOfCatchingFire < 10:
            case ForestSquareState.Diseased when chanceOfCatchingFire < 30:
            case ForestSquareState.Rotten:
                UpdateState(ForestSquareState.OnFire);
                break;
        }
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

        switch (_age)
        {
            case <= 10:
                UpdateState(ForestSquareState.Sapling);
                break;
            case <= 30:
                UpdateState(ForestSquareState.Juvenile);
                break;
            case <= 80:
                UpdateState(ForestSquareState.Mature);
                break;
            case <= 90:
                UpdateState(ForestSquareState.Diseased);
                break;
            default:
                UpdateState(ForestSquareState.Rotten);
                break;
        }
    }

    private void UpdateState(ForestSquareState state)
    {
        State = state;
        _timeAsState = 0;
    }
    
    public override string ToString()
    {
        return $"Tree - x:{Position.x} y:{Position.y} - {State}";
    }
}