using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class Viewport
    {
        private readonly List<Viewport> _connections = [];

        public Rect Dest;

        public float X
        {
            get => Dest.X;
            set => Dest.X = value;
        }
        public float Y
        {
            get => Dest.Y;
            set => Dest.Y = value;
        }
        public float W
        {
            get => Dest.W;
            set => Dest.W = value;
        }
        public float H
        {
            get => Dest.H;
            set => Dest.H = value;
        }

        public Viewport()
        {
            Dest = Rect.Zero;
        }

        public Viewport(float x = 0, float y = 0, float w = 0, float h = 0)
        {
            Dest = new Rect(x, y, w, h);
        }

        public Viewport(Rect dest)
        {
            Dest = dest;
        }

        public void Init()
        {

        }

        public void SetSize(Point size)
        {
            Dest.Size = size;
        }

        public void Use()
        {
            GL.Viewport((int)X, (int)Y, (int)W, (int)H);
        }
    }
}
