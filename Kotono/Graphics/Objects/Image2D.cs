using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using System.IO;

namespace Kotono.Graphics.Objects
{
    public class Image2D : ITexture2D
    {
        public readonly int Handle;

        private readonly TextureUnit _unit;

        public Image2D(int glHandle, TextureUnit unit)
        {
            Handle = glHandle;
            _unit = unit;
        }

        public static Image2D LoadFromFile(string path, TextureUnit unit)
        {
            int handle = GL.GenTexture();

            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Image2D(handle, unit);
        }

        public void Use()
        {
            GL.ActiveTexture(_unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Draw()
        {
            Use();
        }

        public Vector2 Position 
        { 
            get; 
            set; 
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public float Angle 
        { 
            get; 
            set; 
        }

        public bool IsDraw
        {
            get;
            set;
        }

    }
}
