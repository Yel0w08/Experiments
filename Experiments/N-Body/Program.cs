using N_Body.Models;
using N_Body.Render;
using System.Reflection;
using N_Body.Math;



BodyPotitionCalculators _BodyPotitionCalculators = new BodyPotitionCalculators();
Simulation _simulation = new Simulation();
ImRender _ImRender = new ImRender(_simulation, _BodyPotitionCalculators);

_ImRender.Initalize();
