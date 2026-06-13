using N_Body.Models;

public class Simulation
{
    public List<Body> Bodies { get; set; }

    public Simulation()
    {
        Bodies = new List<Body>();
        Bodies.Add(new Body { name = "Star", mass = 1e30, X = 0, Y = 0, Z = 0 });
        Bodies.Add(new Body { name = "Cool Star", mass = 1e28, X = 1e11, Y = 0, Z = 0 });

    }
}