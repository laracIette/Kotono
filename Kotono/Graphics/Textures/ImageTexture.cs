using Kotono.Utils.Exceptions;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using IO = System.IO;

namespace Kotono.Graphics.Textures
{
    internal class ImageTexture : ITexture
    {
        private static readonly Dictionary<string, int> _handles = [];

        internal string Path { get; }

        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; } = TextureUnit.Texture0;

        internal ImageTexture(string path)
        {
            if (!_handles.TryGetValue(path, out int handle))
            {
                ExceptionHelper.ThrowIf(!IsValidPath(path), $"Image path '{path}' isn't a valid image path");

                handle = GL.GenTexture();
                Handle = handle;

                Use();

                var imageData = ImageData.Parse(path, true);

                bool isAlpha = imageData.Size.Product * 4 == imageData.Bytes.Length;

                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    isAlpha ? PixelInternalFormat.Rgba : PixelInternalFormat.Rgb,
                    imageData.Size.X,
                    imageData.Size.Y,
                    0,
                    isAlpha ? PixelFormat.Rgba : PixelFormat.Rgb,
                    PixelType.UnsignedByte,
                    imageData.Bytes
                );

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                ITexture.Unbind(TextureTarget.Texture2D);

                _handles[path] = handle;
            }

            Path = path;
            Handle = handle;
        }

        public void Bind()
            => ITexture.Bind(TextureTarget.Texture2D, Handle);

        public void Use()
            => ITexture.Use(TextureTarget.Texture2D, TextureUnit, Handle);

        public void Delete()
            => ITexture.Delete(Handle);

        internal static bool IsValidPath(string path) 
            => IO.File.Exists(path)
            && IO.Path.GetExtension(path.ToLower()) is string extension
            && (extension == ".jpeg" || extension == ".jpg" || extension == ".png");

        internal static void DisposeAll()
        {
            ITexture.Unbind(TextureTarget.Texture2D);
            GL.DeleteTextures(_handles.Values.Count, [.. _handles.Values]);
        }

        public override string ToString()
            => $"Path: {Path}, Handle: {Handle}, TextureUnit: {TextureUnit}";
    }
}
