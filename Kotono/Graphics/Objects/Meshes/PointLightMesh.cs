using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class PointLightMesh : Mesh
    {
        public override Model Model { get; set; } = new Model(
            Path.FromAssets(@"Meshes\sphere.obj"),
            PointLightShader.Instance
        );

        public override Shader Shader => PointLightShader.Instance;

        public override void Update()
        {
            base.Update();

            if (Parent != null)
            {
                Color = Parent.Color;
            }
        }

        public override void UpdateShader()
        {
            if (Shader is PointLightShader pointLightShader)
            {
                pointLightShader.SetModel(Transform.Model);
                pointLightShader.SetColor(Color);
            }
        }

        public override void Draw()
        {
            Model.Draw();
        }
    }
}
