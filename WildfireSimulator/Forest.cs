namespace WildfireSimulator;

public class Forest
{
    private readonly int _width;
    private readonly int _height;
    private const int InitialTreeDensity = 90;
    private const int LightningStrikeRadius = 2;

    public Dictionary<(int X, int Y), IForestSquare> ForestSquares { get; }
    
    public Forest(int width, int height)
    {
        _width = width;
        _height = height;
        ForestSquares = new();
        
        PlantTrees();
        LightningStrike();
    }

    public void NextDay()
    {
        SpreadFire();
    }

    public void NextYear()
    {
        ClearBurntTrees();
        LightningStrike();
    }
    
    public void ClearBurntTrees()
    {
        foreach (var tree in ForestSquares.OnFireTrees())
        {
            ForestSquares[(tree.Key.X, tree.Key.Y)] = new ForestFloor((tree.Key.X, tree.Key.Y));
        }
    }

    private void PlantTrees()
    {
        foreach (var x in Enumerable.Range(0, _width))
        {
            foreach (var y in Enumerable.Range(0, _height))
            {
                var ran = Random.Shared.Next(100);
                
                ForestSquares.Add(
                    (x, y), 
                    ran < InitialTreeDensity
                        ? new Tree((x, y))
                        : new ForestFloor((x, y))
                );
            }
        }
    }

    private void LightningStrike()
    {
        var strikeCenter = GetRandomSquare();

        while (strikeCenter.GetType() != typeof(Tree))
        {
            strikeCenter = GetRandomSquare();
        }

        var affectedTrees = ForestSquares.Where(s =>
                Math.Ceiling(Math.Pow(s.Key.X - strikeCenter.Position.x, 2) + Math.Ceiling(Math.Pow(s.Key.Y - strikeCenter.Position.y, 2))) <= Math.Ceiling(Math.Pow(LightningStrikeRadius, 2)))
            .Select(s => s.Value);
        
        foreach (var tree in affectedTrees)
        {
            tree.CatchFire();
        }
    }

    private IForestSquare GetRandomSquare()
    {
        var x = Random.Shared.Next(0, _width);
        var y = Random.Shared.Next(0, _height);

        return ForestSquares[(x, y)];
    }

    private void SpreadFire()
    {
        var neighbors = new HashSet<IForestSquare>();
        
        foreach (var tree in ForestSquares.OnFireTrees())
        {
            var xMin = tree.Key.X - 1;
            var xMax = tree.Key.X + 1;
            var yMin = tree.Key.Y - 1;
            var yMax = tree.Key.Y + 1;

            neighbors.UnionWith(ForestSquares
                .Where(s => xMin <= s.Key.X && xMax >= s.Key.X && yMin <= s.Key.Y && yMax >= s.Key.Y)
                .Select(s => s.Value));
        }

        foreach (var tree in neighbors)
        {
            tree.CatchFire();
        }
    }
}