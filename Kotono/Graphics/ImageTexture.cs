using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class ImageTexture : ITexture
    {
        private static readonly Dictionary<string, int> _handles = [];

        internal string Path { get; }

        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; }

        internal ImageTexture(string path)
        {
            Path = path;

            if (!_handles.TryGetValue(path, out int handle))
            {
                handle = GL.GenTexture();

                Use();

                var imageData = ImageData.GetFrom(path);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Size.X, imageData.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData.Bytes);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                ITexture.Unbind();

                _handles[path] = handle;
            }

            Handle = handle;
        }

        public void Bind() => ITexture.Bind(Handle);

        public void Use() => ITexture.Use(Handle, TextureUnit);

        public void Draw() => ITexture.Draw(Handle, TextureUnit);

        public void Delete() => ITexture.Delete(Handle);

        internal static void DisposeAll()
        {
            ITexture.Unbind();
            GL.DeleteTextures(_handles.Values.Count, [.. _handles.Values]);
        }
    }
}
