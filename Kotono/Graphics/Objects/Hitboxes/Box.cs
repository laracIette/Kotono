using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Vector Position { get; set; } = Vector.Zero;

        public Vector Rotation { get; set; } = Vector.Zero;  

        public Vector Scale { get; set; } = Vector.One;

        public Vector Color { get; set; } = Vector.One;

        public List<IHitbox> Collisions { get; set; } = new();

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
            var model =
                Matrix4.CreateScale((Vector3)Scale)
                //* Matrix4.CreateRotationX(Rotation.X)
                //* Matrix4.CreateRotationY(Rotation.Y)
                //* Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation((Vector3)Position);

            KT.SetShaderVector(ShaderType.Hitbox, "color", Color);
            KT.SetShaderMatrix4(ShaderType.Hitbox, "model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public bool Collides(IHitbox h)
        {
            return (Math.Abs(Position.X - h.Position.X) <= (Scale.X + h.Scale.X) / 2)
                && (Math.Abs(Position.Y - h.Position.Y) <= (Scale.Y + h.Scale.Y) / 2)
                && (Math.Abs(Position.Z - h.Position.Z) <= (Scale.Z + h.Scale.Z) / 2);
        }

        public bool IsColliding()
        {
            return Collisions.Any(Collides);
        }
    }
}