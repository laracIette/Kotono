using Kotono.Graphics.Shaders;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape2D : Object2D
    {
        private static readonly Object3DShader _shader = (Object3DShader)ShaderManager.Shaders["shape2D"];

        private readonly PrimitiveType _mode;

        private readonly VertexArraySetup _vertexArraySetup = new();

        private bool _hasInitBuffers = false;

        private Point[] _points;

        internal Point this[int index]
        {
            get => _points[index];
            set
            {
                _points[index] = value;
                UpdateBuffers();
            }
        }

        internal Shape2D(Point[] points, Color color, bool isLoop)
        {
            _points = points;
            Color = color;

            _mode = _points.Length switch
            {
                0 => throw new Exception($"error: Points musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => isLoop ? PrimitiveType.LineLoop : PrimitiveType.LineStrip
            };
        }

        internal void AddPoint(Point point)
        {
            _points = [.. _points, point];

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
            _shader.SetModelMatrix(Rect.Model);

            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.Bind();
            GL.DrawArrays(_mode, 0, _points.Length);
        }

        private void UpdateBuffers()
        {
            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.SetData(_points, Point.SizeInBytes);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Point.SizeInBytes, 0);
        }
    }
}
