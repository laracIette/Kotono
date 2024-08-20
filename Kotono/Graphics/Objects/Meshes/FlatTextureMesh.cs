namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh()
        : Mesh("flatTexture", [], Path.FromAssets(@"Meshes\flatTextureMesh.obj"))
    {
        public override void Draw()
        {
            Material.Use();

            _shader.SetModelMatrix(Transform.Model);
            _shader.SetColor(Color);

            Model.Draw();

            Texture.Bind(0);
        }
    }
}
