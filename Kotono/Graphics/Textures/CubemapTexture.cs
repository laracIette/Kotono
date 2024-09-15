using OpenTK.Graphics.OpenGL4;
using IO = System.IO;

namespace Kotono.Graphics.Textures
{
    internal sealed class CubemapTexture : ITexture
    {
        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; } = TextureUnit.Texture0;

        internal CubemapTexture(string path)
        {
            Handle = GL.GenTexture();

            Use();

            string[] faces = ["right", "left", "top", "bottom", "front", "back"];

            for (int i = 0; i < faces.Length; i++)
            {
                var imageData = ImageData.Parse(IO.Path.Combine(path, faces[i] + ".jpg"), false);

                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgb, imageData.Size.X, imageData.Size.Y, 0, PixelFormat.Rgb, PixelType.UnsignedByte, imageData.Bytes);
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            ITexture.Unbind(TextureTarget.TextureCubeMap);
        }

        public void Bind() => ITexture.Bind(TextureTarget.TextureCubeMap, Handle);

        public void Use() => ITexture.Use(TextureTarget.TextureCubeMap, TextureUnit, Handle);

        public void Delete() => ITexture.Delete(Handle);
    }
}
