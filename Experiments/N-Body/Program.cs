using N_Body.Models;
using N_Body.Render;
using System.Reflection;
using N_Body.Math;


ImRender _ImRender = new ImRender();
BodyPotitionCalculators _BodyPotitionCalculators = new BodyPotitionCalculators();
Simulation _simulation = new Simulation();

while (true) 
{
    
    _ImRender.Render();
    _BodyPotitionCalculators.CalculateForces(_simulation.Bodies);
    _BodyPotitionCalculators.UpdatePositions(_simulation.Bodies, 0.01);
    Console.Clear();
    Console.WriteLine($"Star X: {_simulation.Bodies[0].X:F2}  CoolStar X: {_simulation.Bodies[1].X:F2}");

}


Console.ReadLine();