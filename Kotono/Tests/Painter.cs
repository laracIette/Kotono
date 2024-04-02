using Kotono.Graphics;
using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Tests
{
    internal class Painter : Object2D
    {
        private readonly byte[,] _fragments;

        private readonly int _handle;

        internal Painter()
        {
            Dest = Rect.FromAnchor(new Rect(Point.Zero, 100.0f, 50.0f), Anchor.TopLeft);

            _fragments = new byte[(int)Size.X * 4, (int)Size.Y * 4];
            for (int y = 0; y < Size.Y * 4; y++)
            {
                for (int x = 0; x < Size.X * 4; x++)
                {
                    _fragments[x, y] = 255;
                }
            }

            _handle = GL.GenTexture();
            UpdateTexture();
        }

        public override void Update()
        {
            if (Mouse.IsButtonDown(MouseButton.Left))
            {
                if (Rect.Overlaps(Dest, Mouse.Position))
                {
                    _fragments[(int)Mouse.Position.X * 4, (int)Mouse.Position.Y * 4] = 255;
                    UpdateTexture();
                }
            }
        }

        public override void Draw()
        {
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            ShaderManager.Painter.SetMatrix4("model", Dest.Model);

            Texture.Draw(_handle);
        }

        private void UpdateTexture()
        {
            Texture.Bind(_handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)Size.X, (int)Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, (int)Size.X, (int)Size.Y, PixelFormat.Rgba, PixelType.UnsignedByte, _fragments);
            
            Texture.Bind(0);
        }
    }
}
