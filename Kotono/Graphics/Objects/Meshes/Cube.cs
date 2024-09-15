using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class Cube : Mesh
    {
        internal Cube()
        {
            Model = new Model(Path.FromAssets(@"Meshes\cube.obj"));

            Material = new Material
            {
                Albedo = new MaterialTexture(Path.FromAssets(@"Meshes\cube_diff.png")),
            };

            Shader = LightingPBRShader.Instance;
        }
    }
}
