using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System.Collections.Generic;
using IO = System.IO;

namespace Kotono.Graphics
{
    internal class Texture
    {
        private static readonly Dictionary<string, int> _textures = [];

        internal string Path { get; }

        internal int Handle { get; }

        internal TextureUnit Unit { get; }

        internal Texture(string path, TextureUnit unit = TextureUnit.Texture0)
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

            Path = path;
            Handle = _textures[path];
            Unit = unit;
        }

        internal void Use()
        {
            GL.ActiveTexture(Unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public static void DisposeAll()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);

            foreach (var handle in _textures.Values)
            {
                GL.DeleteTexture(handle);
            }
        }
    }
}
