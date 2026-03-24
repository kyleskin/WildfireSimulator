namespace WildfireSimulator;

public class Simulation
{
   public readonly Forest Forest;

    public Simulation(int width, int height)
    {
        Forest = new Forest(width, height);
    }
}