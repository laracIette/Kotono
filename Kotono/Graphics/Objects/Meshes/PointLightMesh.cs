namespace Kotono.Graphics.Objects.Meshes
{
    internal class PointLightMesh()
        : Mesh("pointLight", [], Path.FromAssets(@"Meshes\sphere.obj"))
    {
        public override void Update()
        {
            base.Update();

            if (Parent != null)
            {
                Color = Parent.Color;
            }
        }

        public override void Draw()
        {
            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            Model.Draw();
        }
    }
}
