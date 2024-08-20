using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class Cube : Mesh
    {
        public Cube()
            : base("lighting", [], Path.FromAssets(@"Meshes\cube.obj"))
        {
            Material.Albedo = new MaterialTexture(Path.FromAssets(@"Meshes\cube_diff.png"), TextureUnit.Texture0);
        }
    }
}
