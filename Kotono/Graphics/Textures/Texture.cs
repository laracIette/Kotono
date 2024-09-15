using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Textures
{
    internal sealed class Texture : ITexture
    {
        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; } = TextureUnit.Texture0;

        internal Texture()
        {
            Handle = GL.GenTexture();

            Use();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            ITexture.Unbind(TextureTarget.Texture2D);
        }

        public void Bind() => ITexture.Bind(TextureTarget.Texture2D, Handle);

        public void Use() => ITexture.Use(TextureTarget.Texture2D, TextureUnit, Handle);

        public void Delete() => ITexture.Delete(Handle);
    }
}
