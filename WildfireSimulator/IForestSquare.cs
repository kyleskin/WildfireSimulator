using Spectre.Console.Rendering;

namespace WildfireSimulator;

public interface IForestSquare : IRenderable
{
    (int x, int y) Position { get; init; }
    ForestSquareState State { get; }
    void AgeYear();
    void CatchFire();
}