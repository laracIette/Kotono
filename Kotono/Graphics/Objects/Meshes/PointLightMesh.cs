using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal sealed class PointLightMesh : Mesh
    {
        public override Shader Shader => PointLightShader.Instance;

        internal PointLightMesh()
        {
            Model = new Model(Path.FromAssets(@"Meshes\sphere.obj"));
        }

        public override void Update()
        {
            base.Update();

            if (Parent is not null)
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

        public override void Draw() => Model?.Draw();
    }
}
