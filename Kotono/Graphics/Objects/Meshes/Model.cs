using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Model
    {
        private static readonly Dictionary<string, ModelVertices> _modelsVertices = [];

        private readonly ElementBufferObject _elementBufferObject = new();

        private readonly ModelVertices _modelVertices;

        private Shader _shader = NewLightingShader.Instance;

        internal Shader Shader
        {
            get => _shader;
            set
            {
                if (_shader != value)
                {
                    _shader = value;
                    UpdateShaderAndBuffers();
                }
            }
        }

        internal ModelTriangle[] Triangles => _modelVertices.Triangles;

        internal Vector[] Vertices => _modelVertices.Vertices;

        internal Vector Center => _modelVertices.Center;

        internal Model(string path, Shader shader)
        {
            if (!_modelsVertices.TryGetValue(path, out ModelVertices? modelVertices))
            {
                modelVertices = new ModelVertices(path);
                _modelsVertices[path] = modelVertices;
            }

            _modelVertices = modelVertices;

            Shader = shader;

            _elementBufferObject.SetData(_modelVertices.Indices, sizeof(int));
        }

        private void UpdateShaderAndBuffers()
        {
            _shader.Use();
            _modelVertices.VertexArraySetup.Bind();
            _shader.SetVertexAttributesData();
            _elementBufferObject.Bind();
        }

        internal void Draw()
        {
            _modelVertices.VertexArraySetup.Bind();

            _elementBufferObject.Bind();

            GL.DrawElements(PrimitiveType.Triangles, _modelVertices.IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
