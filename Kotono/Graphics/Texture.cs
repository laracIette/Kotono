using Kotono.Graphics.Objects;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Texture
    {
        private static readonly Dictionary<string, int> _textures = [];

        internal string Path { get; }

        internal int Handle { get; }

        internal TextureUnit Unit { get; }

        internal Texture(string path, TextureUnit unit)
        {
            if (!_textures.TryGetValue(path, out int value))
            {
                value = Gen();

                Use(value);

                var imageData = ImageData.GetFrom(path);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Size.X, imageData.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageData.Bytes);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                _textures[path] = value;
            }

            Path = path;
            Handle = value;
            Unit = unit;
        }

        /// <summary>
        /// Generate a new Texture.
        /// </summary>
        internal static int Gen()
        {
            int handle = GL.GenTexture();
            Use(handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bind(0);

            return handle;
        }

        internal static void Bind(int handle) => GL.BindTexture(TextureTarget.Texture2D, handle);

        internal static void Use(int handle) => Use(handle, TextureUnit.Texture0);

        internal static void Use(int handle, TextureUnit unit)
        {
            GL.ActiveTexture(unit);

            Bind(handle);
        }

        internal void Use() => Use(Handle, Unit);

        internal static void Draw(int handle) => Draw(handle, TextureUnit.Texture0);

        internal static void Draw(int handle, TextureUnit unit)
        {
            Use(handle, unit);

            SquareVertices.Draw();

            Bind(0);
        }

        internal void Draw() => Draw(Handle, Unit);

        internal static void DisposeAll()
        {
            Bind(0);

            GL.DeleteTextures(_textures.Values.Count, [.. _textures.Values]);
        }
    }
}
