﻿using Kotono.Graphics.Objects;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using System.Collections.Generic;
using System.Linq;
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
            if (!_textures.TryGetValue(path, out int value))
            {
                value = GL.GenTexture();

                GL.ActiveTexture(TextureUnit.Texture0);
                Bind(value);

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

                _textures[path] = value;
            }

            Path = path;
            Handle = value;
            Unit = unit;
        }

        internal static void Bind(int handle) => GL.BindTexture(TextureTarget.Texture2D, handle);

        internal static void Use(int handle, TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);

            Bind(handle);
        }

        internal void Use() => Use(Handle, Unit);

        internal static void Draw(int handle, TextureUnit unit = TextureUnit.Texture0)
        {
            Use(handle, unit);

            SquareVertices.Draw();

            Bind(0);
        }

        internal void Draw() => Draw(Handle, Unit);

        internal static void DisposeAll()
        {
            Bind(0);

            GL.DeleteTextures(_textures.Values.Count, _textures.Values.ToArray());
        }
    }
}
