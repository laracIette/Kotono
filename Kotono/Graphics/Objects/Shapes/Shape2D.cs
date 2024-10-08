﻿using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Shapes
{
    internal sealed class Shape2D : Object2D
    {
        private readonly VertexArraySetup _vertexArraySetup = new();

        private PrimitiveType _mode;

        private bool _isLoop;

        private bool _hasInitBuffers = false;

        private Point[] _points;

        public override Shader Shader => Shape2DShader.Instance;

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
            ExceptionHelper.ThrowIf(_points.Length == 0, "_points musn't be empty");

            _mode = _points.Length switch
            {
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

        public override void UpdateShader()
        {
            if (Shader is Shape2DShader shape2DShader)
            {
                shape2DShader.SetColor(Color);
                shape2DShader.SetModel(Rect.Model);
            }
        }

        public override void Draw()
        {
            _vertexArraySetup.VertexArrayObject.Bind();
            GL.DrawArrays(_mode, 0, _points.Length);
        }

        private void UpdateBuffers()
        {
            _vertexArraySetup.SetData(_points, Point.SizeInBytes);
            _vertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(Shape2DShader.Instance);
        }
    }
}
