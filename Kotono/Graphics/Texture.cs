using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal class Texture : ITexture
    {
        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; }

        internal Texture()
        {
            Handle = GL.GenTexture();

            Use();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            ITexture.Unbind();
        }

        public void Bind() => ITexture.Bind(Handle);

        public void Use() => ITexture.Use(Handle, TextureUnit);

        public void Draw() => ITexture.Draw(Handle, TextureUnit);

        public void Delete() => ITexture.Delete(Handle);
    }
}
