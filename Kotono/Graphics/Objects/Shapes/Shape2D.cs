using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape2D : Object2D
    {
        private readonly VertexArraySetup _vertexArraySetup = new();

        private PrimitiveType _mode;

        private bool _isLoop;

        private bool _hasInitBuffers = false;

        private Point[] _points;

        public override Shader Shader => ShaderManager.Shaders["shape2D"];

        internal Point this[int index]
        {
            get => _points[index];
            set
            {
                _points[index] = value;
                UpdateBuffers();
            }
        }

        internal bool IsLoop
        {
            get => _isLoop;
            set
            {
                _isLoop = value;
                UpdateMode();
            }
        }

        internal Shape2D(Point[] points)
        {
            _points = points;
            UpdateMode();
        }

        internal void AddPoint(Point point)
        {
            _points = [.. _points, point];
            UpdateBuffers();
            UpdateMode();
        }

        private void UpdateMode()
        {
            _mode = _points.Length switch
            {
                0 => throw new Exception($"error: Points musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => IsLoop ? PrimitiveType.LineLoop : PrimitiveType.LineStrip
            };
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
            if (Shader is Object3DShader object3DShader)
            {
                object3DShader.SetColor(Color);
                object3DShader.SetModelMatrix(Rect.Model);
            }

            _vertexArraySetup.Bind();
            GL.DrawArrays(_mode, 0, _points.Length);
        }

        private void UpdateBuffers()
        {
            _vertexArraySetup.SetData(_points, Point.SizeInBytes);
            Shader.SetVertexAttributeData(0, 2, VertexAttribPointerType.Float, Point.SizeInBytes, 0);
        }
    }
}
