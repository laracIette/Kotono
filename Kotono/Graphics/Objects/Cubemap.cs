using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Shaders;
using Kotono.Graphics.Textures;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal sealed class Cubemap : Drawable, ICubemap
    {
        private static readonly Model _model = new(Path.FromAssets(@"Meshes\cube.obj"));

        internal CubemapTexture Texture { get; set; } = new(Path.FromAssets(@"Default\Textures\Cubemap"));

        public override Shader Shader => CubemapShader.Instance;

        public override bool IsHovered => false;

        public override bool IsActive => false;

        static Cubemap() => _model.SetVertexAttributesLayout(CubemapShader.Instance);

        public override void UpdateShader()
        {
            if (Shader is CubemapShader cubemapShader)
            {
                cubemapShader.SetSkybox(Texture.TextureUnit);
            }
        }

        public override void Draw()
        {
            Texture.Use();

            _model.Draw();

            ITexture.Unbind(TextureTarget.TextureCubeMap);
        }
    }
}
