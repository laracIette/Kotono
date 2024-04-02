using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape : Object3D<ShapeSettings>, IShape
    {
        private readonly PrimitiveType _mode;

        private int _vertexArrayObject;

        private int _vertexBufferObject;

        private bool _hasInitBuffers = false;

        public Vector[] Vertices { get; }

        internal Shape(ShapeSettings settings)
            : base(settings)
        {
            IsDraw = false;
            Vertices = settings.Vertices;
            Color = settings.Color;

            _mode = Vertices.Length switch
            {
                0 => throw new Exception($"error: Vertices musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => PrimitiveType.LineLoop
            };
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
            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", Transform.Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(_mode, 0, Vertices.Length);
        }

        private void InitBuffers()
        {
            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // Create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vector.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);

            int locationAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }
    }
}
