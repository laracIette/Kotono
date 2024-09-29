using Kotono.Graphics.Shaders;
using Kotono.Graphics.Textures;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class Image : Object2D
    {
        internal ImageTexture Texture { get; set; } = new(Path.FromAssets(@"Default\Textures\image.jpg"));

        public override Shader Shader => ImageShader.Instance;

        public override void UpdateShader()
        {
            if (Shader is ImageShader imageShader)
            {
                imageShader.SetModel(Rect.Model);
                imageShader.SetColor(Color);
                imageShader.SetTexSampler(Texture.TextureUnit);
            }
        }

        public override void Draw()
        {
            Texture.Use();

            SquareVertices.Draw();

            ITexture.Unbind(TextureTarget.Texture2D);
        }

        public override string ToString()
            => $"{base.ToString()}, Texture: {{{Texture}}}";
    }
}
