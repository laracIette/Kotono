using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using System.Linq;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class Model
    {
        private readonly ModelData[] _modelDatas;

        internal string Path { get; }

        internal ModelTriangle[] Triangles => [.. _modelDatas.SelectMany(m => m.Triangles)];

        internal Vector[] Vertices => [.. _modelDatas.SelectMany(m => m.Vertices)];

        internal Vector Center => Vector.Avg([.. _modelDatas.Select(m => m.Center)]);

        internal Model(string path)
        {
            Path = path;

            _modelDatas = ModelData.Parse(path);
        }

        internal void SetVertexAttributesLayout(Shader shader)
        {
            foreach (var modelData in _modelDatas)
            {
                modelData.VertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(shader);
            }
        }

        internal void Draw()
        {
            foreach (var modelData in _modelDatas)
            {
                modelData.Draw();
            }
        }
    }
}
