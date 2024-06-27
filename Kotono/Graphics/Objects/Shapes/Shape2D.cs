using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape2D : Object2D<Shape2DSettings>
    {
        private readonly PrimitiveType _mode;

        private readonly int _vertexArrayObject;

        private readonly int _vertexBufferObject;

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

        internal int Length => _points.Length;

        internal Shape2D(Shape2DSettings settings)
            : base(settings)
        {
            _points = settings.Points;
            Color = settings.Color;

            _mode = Length switch
            {
                0 => throw new Exception($"error: Points musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => settings.IsLoop ? PrimitiveType.LineLoop : PrimitiveType.LineStrip
            };

            _vertexArrayObject = GL.GenVertexArray();

            _vertexBufferObject = GL.GenBuffer();
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
            ShaderManager.Shaders["shape2D"].SetColor("color", Color);
            ShaderManager.Shaders["shape2D"].SetMatrix4("model", Rect.Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(_mode, 0, Length);
        }

        private void UpdateBuffers()
        {
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Length * Point.SizeInBytes, _points, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Point.SizeInBytes, 0);
        }
    }
}
