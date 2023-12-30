using Kotono.Graphics.Objects;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class Viewport : Object2D
    {
        private readonly List<Viewport> _connections = [];

        public Viewport(Rect dest)
            : base() 
        {
            Dest = dest;
        }

        public void Use()
        {
            ComponentManager.ActiveViewport = this;
            GL.Viewport((int)X, (int)(KT.Dest.H - Y - H), (int)W, (int)H);
        }
    }
}
