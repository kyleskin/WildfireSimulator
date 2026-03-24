using Spectre.Console;
using Spectre.Console.Rendering;

namespace WildfireSimulator;

public static class ForestSquareRenderer
{
    private const int Width = 4;

    public static Measurement Measure()
    {
        return new Measurement(Width, Width);
    }

    public static IEnumerable<Segment> Render(ForestSquareState state, RenderOptions options)
    {
        // Use rounded half-circles if Unicode is supported, otherwise spaces
        const string leftCap = "\uE0B6";
        const string rightCap = "\uE0B4";
  
        var style = new Style(GetStyle(state).Background);

        if (state == ForestSquareState.Empty)
        {
            yield return new Segment("  ");
            
        }
        else
        {
            if (options.Capabilities.Unicode)
            {
                yield return new Segment(leftCap, style);
                yield return new Segment(rightCap, style);
            }
            else
            {
                yield return new Segment(" A ", style);
            }
        }
    }
    
    private static Style GetStyle(ForestSquareState state)
    {
        return state switch
        {
            ForestSquareState.Sapling => new Style(background: Color.SpringGreen1),
            ForestSquareState.Juvenile => new Style(background: Color.SpringGreen4),
            ForestSquareState.Mature => new Style(background: Color.DarkSeaGreen4_1),
            ForestSquareState.Rotten => new Style(background: Color.Gray),
            ForestSquareState.Diseased => new Style(background: Color.LightPink4),
            ForestSquareState.OnFire => new Style(background: Color.Red),
            ForestSquareState.Empty => new Style(background: Color.Black),
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Invalid Forest State {state}")
        };
    }
}