using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Shapes;
using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Tests.Painter
{
#if false
    internal class Painter : Object2D
    {
        private readonly byte[] _fragments;

        private readonly int _handle;

        private new PointI Size { get; }

        internal Painter()
        {
            Rect = Rect.FromAnchor(new Rect(Point.Zero, 1000.0f, 500.0f), Anchor.TopLeft);

            Size = (PointI)Rect.Size;
            _fragments = new byte[Size.Product * 4];

            for (int i = 0; i < Size.Product * 4; i += 4)
            {
                _fragments[i + 3] = 255;
            }

            _handle = Texture.Gen();

            UpdateTexture();
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
            ShaderManager.Painter.SetMatrix4("model", Rect.Model);

            Texture.Draw(_handle);
        }

        private void UpdateTexture()
        {
            Texture.Use(_handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Size.X, Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _fragments);

            Texture.Unbind();
        }

        private void OnLeftButtonDown()
        {
            if ((Mouse.Delta != Point.Zero) && Rect.Overlaps(Rect, Mouse.Position))
            {
                int index = (int)((Size.Y - Mouse.Position.Y - 1) * Size.X + Mouse.Position.X) * 4;

                _fragments[index + 0] = 255;
                _fragments[index + 1] = 255;
                _fragments[index + 2] = 255;
                //_fragments[index + 3] = 255;

                UpdateTexture();
            }
        }
    }
#else

    internal class Canvas : Object2D
    {
        private Shape3D? _shape = null;

        public override void Update()
        {
            if (Mouse.IsButtonDown(MouseButton.Left))
            {
                OnLeftButtonDown();
            }
        }

        private void OnLeftButtonDown()
        {
            if (!Mouse.WasButtonDown(MouseButton.Left))
            {
                _shape = new Shape3D([]);
            }
        }
    }
#endif
}
