using System;
using System.Collections.Generic;
using System.Text;
using N_Body.Models;


namespace N_Body.Math
{
    public class Math
    {



        public void CalculateForces(List<Body> bodies)
        {

            Body b1 = bodies[1];
            Body b2 = bodies[2];
            double masse = b1.mass;
            double posX = b1.X;
            double Gravity = 9.81;
            double Force = Gravity * b1.mass * b2.mass / b1.rayon * b1.rayon ;

        }

    }
}
