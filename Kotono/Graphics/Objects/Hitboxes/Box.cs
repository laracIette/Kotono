using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

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

        private static bool _isFirst = true;

        public Vector3 Position { get; set; } = Vector3.Zero;

        public Vector3 Angle { get; set; } = Vector3.Zero;  

        public Vector3 Scale { get; set; } = Vector3.One;

        public Vector3 Color { get; set; } = Vector3.One;

        public List<int> Collisions { get; set; } = new();

        public Box()
        {
            if (_isFirst)
            {
                _isFirst = false;

                // Create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // create vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

                int positionAttributeLocation = KT.GetShaderAttribLocation(ShaderType.Hitbox, "aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        public void Update() 
        {

        }

        public void Draw()
        {
            KT.SetShaderVector3(ShaderType.Hitbox, "color", Color);
            KT.SetShaderMatrix4(ShaderType.Hitbox, "model", Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public bool Collides(IHitbox h)
            => (Math.Abs(Position.X - h.Position.X) <= (Scale.X + h.Scale.X) / 2)
            && (Math.Abs(Position.Y - h.Position.Y) <= (Scale.Y + h.Scale.Y) / 2)
            && (Math.Abs(Position.Z - h.Position.Z) <= (Scale.Z + h.Scale.Z) / 2);

        private Matrix4 Model =>
            Matrix4.CreateScale(Scale)
            //* Matrix4.CreateRotationX(Angle.X)
            //* Matrix4.CreateRotationY(Angle.Y)
            //* Matrix4.CreateRotationZ(Angle.Z)
            * Matrix4.CreateTranslation(Position);
    }
}