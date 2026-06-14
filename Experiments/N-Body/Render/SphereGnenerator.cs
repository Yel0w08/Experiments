using System;
using System.Collections.Generic;
using System.Text;

namespace N_Body.Render
{
    public  class SphereGnenerator
    {
        public static float[] Generate(float radius, int segments)
        {
            var vertices = new List<float>();

            for (int i = 0; i <= segments; i++)        // latitude
            {
                float phi = MathF.PI * i / segments;
                for (int j = 0; j <= segments; j++)    // longitude
                {
                    float theta = 2 * MathF.PI * j / segments;
                    float x = radius * MathF.Sin(phi) * MathF.Cos(theta);
                    float y = radius * MathF.Cos(phi);
                    float z = radius * MathF.Sin(phi) * MathF.Sin(theta);
                    vertices.Add(x);
                    vertices.Add(y);
                    vertices.Add(z);
                }
            }
            return vertices.ToArray();
        }
        public static uint[] GenerateIndices(int segments)
        {
            var indices = new List<uint>();
            for (int i = 0; i < segments; i++)
            {
                for (int j = 0; j < segments; j++)
                {
                    uint row1 = (uint)(i * (segments + 1) + j);
                    uint row2 = (uint)((i + 1) * (segments + 1) + j);
                    // triangle 1
                    indices.Add(row1);
                    indices.Add(row1 + 1);
                    indices.Add(row2);
                    // triangle 2
                    indices.Add(row2);
                    indices.Add(row1 + 1);
                    indices.Add(row2 + 1);
                }
            }
            return indices.ToArray();
        }

    }
}
