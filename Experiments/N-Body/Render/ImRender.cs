using N_Body.Math;
using Silk.NET;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Text;

namespace N_Body.Render
{
    public class ImRender
    {
        public ImRender(Simulation simulation, BodyPotitionCalculators calc)
        {
            _simulation = simulation;
            _calc = calc;
        }

        private GL _gl;

        private IWindow _window;
        private Simulation _simulation;
        private BodyPotitionCalculators _calc;

        public void Initalize()
        {



            var options = WindowOptions.Default;
            options.Size = new Silk.NET.Maths.Vector2D<int>(800, 600);
            options.Title = "N-Body";


            _window = Window.Create(options);

            _window.Load += () =>
            {
                _gl = GL.GetApi(_window);
                _gl.ClearColor(0, 0, 0, 1); 


            };


            _window.Render += (dt) =>
            {
                _calc.CalculateForces(_simulation.Bodies);
                _calc.UpdatePositions(_simulation.Bodies, dt);
                Console.Clear();
                Console.WriteLine($"Star X: {_simulation.Bodies[0].X:F2}");

            };




            _window.Run();



        }
        public void Render()
        {

        }
    }

}
