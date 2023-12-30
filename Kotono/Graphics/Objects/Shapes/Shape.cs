using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    public class Shape : Object3D, IShape
    {
        public Vector[] Vertices { get; }

        public Color Color;

        public Matrix4 Model => Transform.Model;

        private int _vertexArrayObject;

        private int _vertexBufferObject;

        private bool _hasInitBuffers = false;

        public Shape(Vector[] vertices, Transform transform, Color color)
            : base()
        {
            Vertices = vertices;
            Transform = transform;
            Color = color;
            IsDraw = false;
        }

        public override void Update()
        {
            if (!_hasInitBuffers && IsDraw)
            {
                _hasInitBuffers = true;
                InitBuffers();
            }
        }

        public override void Draw()
        {
            var model =
                Matrix4.CreateScale((Vector3)Scale)
                * Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation((Vector3)Location);

            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, Vertices.Length);
        }

        private void InitBuffers()
        {
            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vector.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);

            int locationAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }
    }
}
