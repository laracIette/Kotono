using Kotono.Graphics.Objects;
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
            WindowComponentManager.ActiveViewport = this;
            GL.Viewport((int)Position.X, (int)(Window.Rect.Size.Y - Position.Y - Size.Y), (int)Size.X, (int)Size.Y);
        }
    }
}
