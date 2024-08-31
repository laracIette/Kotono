using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class PointLightMesh : Mesh
    {
        public override Model Model { get; set; } = new Model(
            Path.FromAssets(@"Meshes\sphere.obj"),
            ShaderManager.Shaders["pointLight"]
        );

        public override Shader Shader { get; set; } = ShaderManager.Shaders["pointLight"];

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
            Shader.SetMatrix4("model", Transform.Model);
            Shader.SetColor("color", Color);

            Model.Draw();
        }
    }
}
