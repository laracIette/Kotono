using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Viewport : Object2D
    {
        private readonly List<Viewport> _connections = [];

        internal Viewport(Object2DSettings settings)
            : base(settings) 
        {
        }

        internal void Use()
        {
            ComponentManager.ActiveViewport = this;
            GL.Viewport((int)X, (int)(KT.Dest.H - Y - H), (int)W, (int)H);
        }
    }
}
