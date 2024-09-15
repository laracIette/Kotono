using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics.Textures
{
    internal interface ITexture
    {
        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; }

        public void Bind();

        public void Use();

        public void Delete();

        internal static void Unbind(TextureTarget textureTarget)
        {
            Bind(textureTarget, 0);
        }

        protected static void Bind(TextureTarget textureTarget, int handle)
        {
            GL.BindTexture(textureTarget, handle);
        }

        protected static void Use(TextureTarget textureTarget, TextureUnit textureUnit, int handle)
        {
            GL.ActiveTexture(textureUnit);
            Bind(textureTarget, handle);
        }

        protected static void Delete(int handle)
        {
            GL.DeleteTexture(handle);
        }
    }
}
