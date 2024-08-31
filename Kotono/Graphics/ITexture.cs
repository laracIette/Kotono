using Kotono.Graphics.Objects;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal interface ITexture
    {
        public int Handle { get; }

        public TextureUnit TextureUnit { get; set; }

        public void Bind();

        public void Use();

        public void Draw();

        public void Delete();

        protected static void Use(int handle, TextureUnit unit)
        {
            GL.ActiveTexture(unit);

            Bind(handle);
        }

        protected static void Draw(int handle, TextureUnit unit)
        {
            Use(handle, unit);

            SquareVertices.Draw();

            Unbind();
        }

        protected static void Bind(int handle) => GL.BindTexture(TextureTarget.Texture2D, handle);

        protected static void Delete(int handle) => GL.DeleteTexture(handle);

        internal static void Unbind() => Bind(0);
    }
}
