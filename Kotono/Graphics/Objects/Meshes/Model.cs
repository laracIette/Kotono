using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Model
    {
        private static readonly Dictionary<string, ModelVertices> _models = [];

        private readonly ElementBufferObject _elementBufferObject = new();

        private readonly ModelVertices _modelVertices;

        private Shader _shader;

        internal Shader Shader
        {
            get => _shader;
            set
            {
                if (_shader != value)
                {
                    _shader = value;

                    _shader.Use();

                    _modelVertices.VertexArraySetup.VertexArrayObject.Bind();

                    BindAttributes();

                    _elementBufferObject.Bind();
                }
            }
        }

        internal Model(string path, Shader shader)
        {
            _shader = shader;

            if (!_models.TryGetValue(path, out ModelVertices? modelVertices))
            {
                modelVertices = new ModelVertices(path);
                _models[path] = modelVertices;
            }

            _modelVertices = modelVertices;

            BindAttributes();

            _elementBufferObject.SetData(_modelVertices.Indices, sizeof(int));
        }

        private void BindAttributes()
        {
            int locationAttributeLocation = Shader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, 0);

            int normalAttributeLocation = Shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalAttributeLocation);
            GL.VertexAttribPointer(normalAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, Vector.SizeInBytes);

            int tangentAttributeLocation = Shader.GetAttribLocation("aTangent");
            GL.EnableVertexAttribArray(tangentAttributeLocation);
            GL.VertexAttribPointer(tangentAttributeLocation, 3, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, Vector.SizeInBytes * 2);

            int texCoordAttributeLocation = Shader.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(texCoordAttributeLocation);
            GL.VertexAttribPointer(texCoordAttributeLocation, 2, VertexAttribPointerType.Float, false, Vertex3D.SizeInBytes, Vector.SizeInBytes * 3);
        }
    }
}
