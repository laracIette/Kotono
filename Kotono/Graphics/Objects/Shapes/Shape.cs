﻿using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape : Object3D<ShapeSettings>
    {
        private readonly PrimitiveType _mode;

        private readonly int _vertexArrayObject;

        private readonly int _vertexBufferObject;

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

        internal int Length => _vertices.Length;

        internal Shape(ShapeSettings settings)
            : base(settings)
        {
            _vertices = settings.Vertices;
            Color = settings.Color;

            _mode = Length switch
            {
                0 => throw new Exception($"error: Vertices musn't be empty."),
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                _ => settings.IsLoop ? PrimitiveType.LineLoop : PrimitiveType.LineStrip
            };

            _vertexArrayObject = GL.GenVertexArray();

            _vertexBufferObject = GL.GenBuffer();
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
            ShaderManager.Shaders["hitbox"].SetColor("color", Color);
            ShaderManager.Shaders["hitbox"].SetMatrix4("model", Transform.Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(_mode, 0, Length);
        }

        private void UpdateBuffers()
        {
            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Length * Vector.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }
    }
}
