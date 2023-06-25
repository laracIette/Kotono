using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Viewport
    {
        private readonly List<Viewport> connections = new();

        public Rect Dest;

        internal Viewport()
            : this(new Rect()) { }

        internal Viewport(float x = 0, float y = 0, float w = 0, float h = 0)
            : this(new Rect(x, y, w, h)) { }

        internal Viewport(Rect dest)
        {
            Dest = dest;
        }

        internal void Init()
        {
        }

        internal void Use()
        {
            GL.Viewport((int)Dest.X, (int)Dest.Y, (int)Dest.W, (int)Dest.H);

            KT.SetCameraAspectRatio(0, Dest.W / Dest.H);
            
            KT.CurrentViewportWidth = Dest.W;
            KT.CurrentViewportHeight = Dest.H;
        }
    }
}
