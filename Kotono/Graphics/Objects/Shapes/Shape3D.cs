﻿using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Shape3D : Object3D
    {
        private readonly VertexArraySetup _vertexArraySetup = new();

        private PrimitiveType _mode;

        private bool _isLoop = false;

        private bool _hasInitBuffers = false;

        private Vector[] _vertices;

        public override Shader Shader => HitboxShader.Instance;

        internal Vector this[int index]
        {
            get => _vertices[index];
            set
            {
                _vertices[index] = value;
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

        internal Shape3D(Vector[] vertices)
        {
            _vertices = vertices;
            UpdateMode();
        }

        internal void AddVertex(Vector vertex)
        {
            _vertices = [.. _vertices, vertex];
            UpdateBuffers();
            UpdateMode();
        }

        private void UpdateMode()
        {
            _mode = _vertices.Length switch
            {
                0 => throw new KotonoException($"Vertices musn't be empty"),
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
            if (Shader is HitboxShader hitboxShader)
            {
                hitboxShader.SetColor(Color);
                hitboxShader.SetModel(Transform.Model);
            }
        }

        public override void Draw()
        {
            _vertexArraySetup.Bind();
            GL.DrawArrays(_mode, 0, _vertices.Length);
        }

        private void UpdateBuffers()
        {
            _vertexArraySetup.SetData(_vertices, Vector.SizeInBytes);
            Shader.SetVertexAttributeData(0, 3, VertexAttribPointerType.Float, Vector.SizeInBytes, 0);
        }
    }
}
