using Kotono.Graphics.Shaders;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape : Object3D
    {
        private static readonly Object3DShader _shader = (Object3DShader)ShaderManager.Shaders["hitbox"];

        private readonly PrimitiveType _mode;

        private readonly VertexArraySetup _vertexArraySetup = new();

        private bool _hasInitBuffers = false;

        private Vector[] _vertices;

        internal Vector this[int index]
        {
            get => _vertices[index];
            set
            {
                _vertices[index] = value;

                UpdateBuffers();
            }
        }

        internal Shape(Vector[] vertices, bool isLoop)
        {
            _vertices = vertices;

            _mode = _vertices.Length switch
            {
                0 => throw new Exception($"error: Vertices musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => isLoop ? PrimitiveType.LineLoop : PrimitiveType.LineStrip
            };
        }

        internal void AddVertex(Vector vertex)
        {
            _vertices = [.. _vertices, vertex];

            UpdateBuffers();
        }

        public override void Update()
        {
            if (!_hasInitBuffers && IsDraw)
            {
                _hasInitBuffers = true;
                UpdateBuffers();
            }
        }

        public override void Draw()
        {
            _shader.SetColor(Color);
            _shader.SetModelMatrix(Transform.Model);

            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.Bind();
            GL.DrawArrays(_mode, 0, _vertices.Length);
        }

        private void UpdateBuffers()
        {
            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.SetData(_vertices, Vector.SizeInBytes);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }
    }
}
