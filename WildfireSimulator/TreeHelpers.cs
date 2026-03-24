namespace WildfireSimulator;

public static class TreeHelpers
{
    public static KeyValuePair<(int X, int Y), IForestSquare>[] OnFireTrees(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => s.Value.State == ForestSquareState.OnFire).ToArray();
}