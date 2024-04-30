using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class PointLightMesh()
        : Mesh(
            JsonParser.Parse<MeshSettings>(Path.ASSETS + @"Meshes\pointLightMesh.json")
        )
    {
        public override void Draw()
        {
            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            Model.Draw();
        }
    }
}
