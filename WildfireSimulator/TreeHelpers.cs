using System.Text;

namespace WildfireSimulator;

public static class TreeHelpers
{
    public static KeyValuePair<(int X, int Y), IForestSquare>[] OnFireTrees(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => s.Value.State == ForestSquareState.OnFire).ToArray();
    
    public static KeyValuePair<(int X, int Y), IForestSquare>[] EmptySquares(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => s.Value.State == ForestSquareState.Empty).ToArray();
    
    public static KeyValuePair<(int X, int Y), IForestSquare>[] DiseasedTrees(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => s.Value.State == ForestSquareState.Diseased).ToArray();
    
    public static KeyValuePair<(int X, int Y), IForestSquare>[] RottenTrees(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => s.Value.State == ForestSquareState.Rotten).ToArray();
    
    public static KeyValuePair<(int X, int Y), IForestSquare>[] GrownTrees(
        this Dictionary<(int X, int Y), IForestSquare> trees) =>
        trees.Where(s => 
                            s.Value.State != ForestSquareState.Diseased 
                         && s.Value.State != ForestSquareState.OnFire
                         && s.Value.State != ForestSquareState.Rotten
                         && s.Value.State != ForestSquareState.Empty).ToArray();

    public static string ToDisplay(this Dictionary<(int X, int Y), IForestSquare> forestSquares)
    {
        var sb = new StringBuilder();
        sb.Append($"Forest Floor: {forestSquares.EmptySquares().Length}");
        sb.Append($"\nGrown Trees: {forestSquares.GrownTrees().Length}");
        sb.Append($"\nOn Fire: {forestSquares.OnFireTrees().Length}");
        sb.Append("\n---------------------------------------------------------");

        return sb.ToString();
    }
}