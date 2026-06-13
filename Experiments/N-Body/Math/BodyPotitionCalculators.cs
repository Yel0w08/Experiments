using System;
using System.Collections.Generic;
using System.Text;
using N_Body.Models;


namespace N_Body.Math
{
    public class BodyPotitionCalculators
    {



        public void CalculateForces(List<Body> bodies)
        {


            double Gravity = 6.674e-11;


            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    Body b1 = bodies[i];
                    Body b2 = bodies[j];
                    //double posX = b1.X;

                    double dx = b2.X - b1.X;
                    double dy = b2.Y - b1.Y;
                    double dz = b2.Z - b1.Z;

                    double r = System.Math.Sqrt(dx * dx + dy * dy + dz * dz);

                    double nx = dx / r;
                    double ny = dy / r;
                    double nz = dz / r;


                    double Force = Gravity * b1.mass * b2.mass / (r * r);


                    double fx = Force * nx;
                    double fy = Force * ny;
                    double fz = Force * nz;

                    b1.aX += fx / b1.mass;
                    b1.aY += fy / b1.mass;
                    b1.aZ += fz / b1.mass;

                    b2.aX -= fx / b2.mass;
                    b2.aY -= fy / b2.mass;
                    b2.aZ -= fz / b2.mass;
                }
            }
        }
        public void UpdatePositions(List<Body> bodies, double deltaTime)
        {
            foreach (var body in bodies)
            {
                body.vX += body.aX * deltaTime;
                body.vY += body.aY * deltaTime;
                body.vZ += body.aZ * deltaTime;
                body.X += body.vX * deltaTime;
                body.Y += body.vY * deltaTime;
                body.Z += body.vZ * deltaTime;

                body.aX = 0;
                body.aY = 0;
                body.aZ = 0;
            }
        }

    }
}
