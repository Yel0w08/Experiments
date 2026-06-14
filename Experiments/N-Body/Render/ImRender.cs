using ImGuiNET;
using N_Body.Math;
using Silk.NET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Text;
using static Silk.NET.Core.Native.WinString;


namespace N_Body.Render
{
    public class ImRender
    {
        public ImRender(Simulation simulation, BodyPotitionCalculators calc)
        {
            _simulation = simulation;
            _calc = calc;
        }

        private float _dt = 1000.0f;

        private GL _gl;
        private uint _vao;
        private uint _vbo;
        private uint _program;
        private IWindow _window;
        private Simulation _simulation;
        private BodyPotitionCalculators _calc;
        private ImGuiController _imguiController;
        private IInputContext _input;
        private float _camDistance = 3.0f;
        private float _camAngleX = 0.0f;
        private float _camAngleY = 0.0f;
        private float _scale = 1e11f;
        private int _uView;
        private int _uProjection;

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

                string vertSrc = File.ReadAllText("Shaders/body.vert");
                string fragSrc = File.ReadAllText("Shaders/body.frag");

                uint vert = _gl.CreateShader(ShaderType.VertexShader);
                _gl.ShaderSource(vert, vertSrc);
                _gl.CompileShader(vert);

                uint frag = _gl.CreateShader(ShaderType.FragmentShader);
                _gl.ShaderSource(frag, fragSrc);
                _gl.CompileShader(frag);

                _program = _gl.CreateProgram();

                _gl.AttachShader(_program, vert);
                _gl.AttachShader(_program, frag);
                _gl.LinkProgram(_program);

                _vao = _gl.GenVertexArray();
                _vbo = _gl.GenBuffer();
                _input = _window.CreateInput();


                _imguiController = new ImGuiController(_gl, _window, _input);


                _input.Mice[0].Scroll += (mouse, wheel) =>
                {
                    _scale *= wheel.Y > 0 ? 0.8f : 1.2f;
                };

                //_input.Mice[0].MouseMove += (mouse, delta) =>
                //{
                //    if (mouse.IsButtonPressed(MouseButton.Right))
                //    {
                //        _camAngleY += delta.X * 0.01f;
                //        _camAngleX += delta.Y * 0.01f;
                //    }
                //};


                _uView = _gl.GetUniformLocation(_program, "uView");
                _uProjection = _gl.GetUniformLocation(_program, "uProjection");

            };

            _window.Render += (deltaTime) =>
            {

                _gl.Clear(ClearBufferMask.ColorBufferBit);

                _calc.CalculateForces(_simulation.Bodies);
                _calc.UpdatePositions(_simulation.Bodies, _dt);



                float[] positions = new float[_simulation.Bodies.Count * 3];
                for (int i = 0; i < _simulation.Bodies.Count; i++)
                {
                    positions[i * 3] = (float)(_simulation.Bodies[i].X / _scale);
                    positions[i * 3 + 1] = (float)(_simulation.Bodies[i].Y / _scale);
                    positions[i * 3 + 2] = (float)(_simulation.Bodies[i].Z / _scale);
                }

                _gl.BindVertexArray(_vao);

                _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(positions.Length * sizeof(float)), positions, BufferUsageARB.DynamicDraw);


                _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

                _gl.EnableVertexAttribArray(0);

                _gl.UseProgram(_program);

          var proj = Matrix4X4.CreatePerspectiveFieldOfView(
                    MathF.PI / 4f, // 45°
                    800f / 600f,   // aspect ratio
                    0.1f,          // near
                    1000f          // far
                );

                var camX = _camDistance * MathF.Sin(_camAngleY) * MathF.Cos(_camAngleX);
                var camY = _camDistance * MathF.Sin(_camAngleX);
                var camZ = _camDistance * MathF.Cos(_camAngleY) * MathF.Cos(_camAngleX);

                var view = Matrix4X4.CreateLookAt(
                    new Vector3D<float>(camX, camY, camZ),
                    new Vector3D<float>(0, 0, 0),
                    new Vector3D<float>(0, 1, 0)
                );


                _gl.UniformMatrix4(_uProjection, 1, false, ref proj.Row1.X);
                _gl.UniformMatrix4(_uView, 1, false, ref view.Row1.X);


                _gl.Enable(EnableCap.ProgramPointSize);

                _imguiController.Update((float)deltaTime);


                _gl.DrawArrays(PrimitiveType.Points, 0, (uint)_simulation.Bodies.Count);

                ImGui.Begin("Controls");
                ImGui.SliderFloat("Speed", ref _dt, 0.001f, 10000.0f);
                if (ImGui.Button("Add Body"))
                {
                    _simulation.AddRandomBody();
                }
                ImGui.Text($"Bodies: {_simulation.Bodies.Count}");

                ImGui.End();


                _imguiController.Render();


            };




            _window.Run();



        }
        public void Render()
        {

        }
    }

}
