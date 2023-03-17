using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public class Distance
    {
        private readonly Vector3[] _vertices;

        private readonly int _vertexArrayObject;

        private readonly int _vertexBufferObject;

        public Distance()
        {
            int segments = 64;
            _vertices = new Vector3[segments + 1];
            for (int i = 0; i <= segments; i++)
            {
                float angle = i / (float)segments * 2.0f * (float)Math.PI;
                _vertices[i] = new Vector3
                {
                    X = 0.0f + 0.5f * (float)Math.Cos(angle),
                    Y = 0.0f + 0.5f * (float)Math.Sin(angle),
                    Z = 0.0f
                };
            }

            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float) * 3, _vertices, BufferUsageHint.StaticDraw);

            int positionAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionAttributeLocation);
            GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
        }
        public void Draw(Vector3 position, Vector3 angle, Vector3 scale, Vector3 color)
        {
            ShaderManager.Hitbox.SetVector3("color", color);

            Matrix4 model = Matrix4.CreateScale(scale)
            * Matrix4.CreateRotationX(angle.X)
            * Matrix4.CreateRotationY(angle.Y)
            * Matrix4.CreateRotationZ(angle.Z)
            * Matrix4.CreateTranslation(position);

            ShaderManager.Hitbox.SetMatrix4("model", model);
            ShaderManager.Hitbox.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Hitbox.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);


            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
        }
    }
}
