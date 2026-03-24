using Spectre.Console;
using WildfireSimulator;

const int width = 120;
const int height = 50;

var simulation = new Simulation(width, height);
var grid = new Table().MinimalistBorder().HideHeaders().HideFooters().HideRowSeparators();

// foreach (var _ in Enumerable.Range(0, 5))
// {
//     foreach (var __ in Enumerable.Range(0, 30))
//     {
//         simulation.Forest.NextDay();
//     }
//     simulation.Forest.NextYear();
// }
// simulation.Forest.NextYear(lastYear: true);


foreach (var _ in Enumerable.Range(0, width))
{
    grid.AddColumn(new("") { Width = 2 });
}

foreach (var y in Enumerable.Range(0, height))
{
    grid.AddRow(simulation.Forest.ForestSquares.Where(s => s.Key.Y == y).Select(s => s.Value));

    if (y < height - 1)
    {
        grid.AddEmptyRow();
    }
}

var forestPanel = new Panel(grid).Padding(2, 1).Header("[green4 bold]Forest[/]", Justify.Center).BorderColor(Color.Green4).DoubleBorder();

AnsiConsole.Live(forestPanel)
    .Start(ctx =>
    {
        foreach (var _ in Enumerable.Range(0, 5))
        {
            foreach (var __ in Enumerable.Range(0, 30))
            {
                simulation.Forest.NextDay();
           
                grid.Rows.Clear();
           
                foreach (var y in Enumerable.Range(0, height))
                {
                    grid.AddRow(simulation.Forest.ForestSquares.Where(s => s.Key.Y == y).Select(s => s.Value));

                    if (y < height - 1)
                    {
                        grid.AddEmptyRow();
                    }
                }
                
                ctx.UpdateTarget(forestPanel);
            }     
            simulation.Forest.NextYear();
        }
        
        simulation.Forest.ClearBurntTrees();
        
        grid.Rows.Clear();
           
        foreach (var y in Enumerable.Range(0, height))
        {
            grid.AddRow(simulation.Forest.ForestSquares.Where(s => s.Key.Y == y).Select(s => s.Value));

            if (y < height - 1)
            {
                grid.AddEmptyRow();
            }
        }
        
        ctx.UpdateTarget(forestPanel);
    });