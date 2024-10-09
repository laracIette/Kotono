using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics
{
    internal sealed class Viewport
    {
        internal PointI Position { get; set; } = PointI.Zero;

        internal PointI Size { get; set; } = PointI.Zero;

        internal void Use() 
            => GL.Viewport(Position.X, -Position.Y, Size.X, Size.Y);
    }
}
