using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System.Collections.Generic;
using IO = System.IO;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class TextureManager
    {
        private static readonly Dictionary<string, int> _textures = new();

        internal static int LoadTexture(string path)
        {
            if (!_textures.ContainsKey(path))
            {
                int handle = GL.GenTexture();

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, handle);

                StbImage.stbi_set_flip_vertically_on_load(1);

                using (IO.Stream stream = IO.File.OpenRead(path))
                {
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                }

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                _textures[path] = handle;
            }

            return _textures[path];
        }

        internal static void UseTexture(int handle, TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);
        }
    }
}
