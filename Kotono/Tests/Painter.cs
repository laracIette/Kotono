using Kotono.Graphics;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Tests
{
    internal class Painter : Object2D
    {
        private readonly byte[] _fragments;

        private readonly int _handle;

        private new PointI Size { get; }

        internal Painter()
        {
            Dest = Rect.FromAnchor(new Rect(Point.Zero, 100.0f, 50.0f), Anchor.TopLeft);

            Size = (PointI)Dest.Size;
            _fragments = new byte[Size.Product * 4];

            for (int i = 0; i < Size.Product * 4; i += 4)
            {
                _fragments[i + 3] = 255;
            }

            _handle = GL.GenTexture();
            UpdateTexture();

            Texture.Use(_handle);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Texture.Bind(0);

        }

        public override void Update()
        {
            if (Mouse.IsButtonDown(MouseButton.Left))
            {
                OnLeftButtonDown();
            }
        }

        public override void Draw()
        {
            ShaderManager.Painter.SetMatrix4("model", Dest.Model);

            Texture.Draw(_handle);
        }

        private void UpdateTexture()
        {
            Texture.Use(_handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Size.X, Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _fragments);

            Texture.Bind(0);
        }

        private void OnLeftButtonDown()
        {
            if ((Mouse.Delta != Point.Zero) && Rect.Overlaps(Dest, Mouse.Position))
            {
                int index = (int)((Size.Y - Mouse.Position.Y - 1) * Size.X + Mouse.Position.X) * 4;
                _fragments[index + 0] = 255;
                _fragments[index + 1] = 255;
                _fragments[index + 2] = 255;
                UpdateTexture();
            }
        }
    }
}
