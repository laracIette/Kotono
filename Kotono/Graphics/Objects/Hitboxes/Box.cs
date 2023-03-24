using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public class Box : IHitbox
    {
        private static readonly float[] _vertices =
        {
            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,

             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,

             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,


            -0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,

             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,


            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f, -0.5f,

             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f, -0.5f,

            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };

        private static int _vertexArrayObject;

        private static int _vertexBufferObject;

        private static int _hitboxShader;

        private static bool _isFirst = true;

        public Vector3 Position { get; set; } = Vector3.Zero;

        public Vector3 Angle { get; set; } = Vector3.Zero;  

        public Vector3 Scale { get; set; } = Vector3.One;

        public Vector3 Color { get; set; } = Vector3.One;

        public Box()
        {
            if (_isFirst)
            {
                _isFirst = false;

                _hitboxShader = KT.CreateShader(new HitboxShader());

                // Create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // create vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

                int positionAttributeLocation = KT.GetShaderAttribLocation(_hitboxShader, "aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        public void Update() { }

        public void Update(Vector3 position, Vector3 angle, Vector3 scale, Vector3 color)
        {
            Position = position;
            Angle = angle;
            Scale = scale;
            Color = color;
        }

        public void Draw()
        {
            KT.SetShaderVector3(_hitboxShader, "color", Color);
            KT.SetShaderMatrix4(_hitboxShader, "model", Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public bool Collides(Box b)
            => (Math.Abs(Position.X - b.Position.X) <= (Scale.X + b.Scale.X) / 2)
            && (Math.Abs(Position.Y - b.Position.Y) <= (Scale.Y + b.Scale.Y) / 2)
            && (Math.Abs(Position.Z - b.Position.Z) <= (Scale.Z + b.Scale.Z) / 2);

        private Matrix4 Model =>
            Matrix4.CreateScale(Scale)
            * Matrix4.CreateRotationX(Angle.X)
            * Matrix4.CreateRotationY(Angle.Y)
            * Matrix4.CreateRotationZ(Angle.Z)
            * Matrix4.CreateTranslation(Position);
    }
}