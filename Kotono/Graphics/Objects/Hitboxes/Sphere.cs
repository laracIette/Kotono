using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public class Sphere : IHitbox
    {
        private const int SEGMENTS = 64;

        private static readonly Vector3[] _vertices = new Vector3[SEGMENTS + 1];

        private static int _vertexArrayObject;

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        public Vector3 Position { get; set; } = Vector3.Zero;

        public Vector3 Angle { get; set; } = Vector3.Zero;

        public Vector3 Scale { get; set; } = Vector3.One;

        public Vector3 Color { get; set; } = Vector3.One;

        public List<int> Collisions { get; set; } = new();

        public Sphere()
        {
            if (_isFirst)
            {
                _isFirst = false;

                for (int i = 0; i <= SEGMENTS ; i++)
                {
                    float angle = i / (float)SEGMENTS * MathHelper.TwoPi;
                    _vertices[i] = new Vector3
                    {
                        X = 0.5f * (float)Math.Cos(angle),
                        Y = 0.5f * (float)Math.Sin(angle),
                        Z = 0f
                    };
                }

                // Create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // create vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vector3.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);

                int positionAttributeLocation = KT.GetShaderAttribLocation(ShaderType.Hitbox, "aPos");
                GL.EnableVertexAttribArray(positionAttributeLocation);
                GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            }
        }

        public void Update() 
        {
        }

        public void Draw()
        {
            DrawCircle(new Vector3(MathHelper.PiOver2, 0f, 0f));
            DrawCircle(new Vector3(0f, MathHelper.PiOver2, 0f));
            DrawCircle(new Vector3(0f, 0f, MathHelper.PiOver2));
        }

        private void DrawCircle(Vector3 angle)
        {
            var Model =
                Matrix4.CreateScale(Scale)
                * Matrix4.CreateRotationX(Angle.X + angle.X)
                * Matrix4.CreateRotationY(Angle.Y + angle.Y)
                * Matrix4.CreateRotationZ(Angle.Z + angle.Z)
                * Matrix4.CreateTranslation(Position);

            KT.SetShaderVector3(ShaderType.Hitbox, "color", Color);
            KT.SetShaderMatrix4(ShaderType.Hitbox, "model", Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
        }

        public bool Collides(IHitbox h)
            => (Math.Abs(Position.X - h.Position.X) <= (Scale.X + h.Scale.X) / 2)
            && (Math.Abs(Position.Y - h.Position.Y) <= (Scale.Y + h.Scale.Y) / 2)
            && (Math.Abs(Position.Z - h.Position.Z) <= (Scale.Z + h.Scale.Z) / 2);

    }
}