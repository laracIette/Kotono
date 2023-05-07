using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Viewport
    {
        private readonly List<Viewport> connections = new();

        private readonly Rect _dest;

        internal float X
        {
            get => _dest.X; 
            set => _dest.X = value;
        }

        internal float Y
        {
            get => _dest.Y;
            set => _dest.Y = value;
        }

        internal float W
        {
            get => _dest.W;
            set => _dest.W = value;
        }

        internal float H
        {
            get => _dest.H;
            set => _dest.H = value;
        }

        internal Viewport()
            : this(new Rect()) { }

        internal Viewport(float x = 0, float y = 0, float w = 0, float h = 0)
            : this(new Rect(x, y, w, h)) { }

        internal Viewport(Rect dest)
        {
            _dest = dest;
        }

        internal void Use()
        {
            GL.Viewport((int)X, (int)Y, (int)W, (int)H);
            
            //KT.SetCameraAspectRatio(0, W / H);

            KT.CurrentViewportWidth = W;
            KT.CurrentViewportHeight = H;
        }
    }
}
