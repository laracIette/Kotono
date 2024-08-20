using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Viewport : IRect
    {
        internal Rect Rect { get; } = Rect.Default;

        public Point BaseSize
        {
            get => Rect.BaseSize;
            set => Rect.BaseSize = value;
        }

        public Point Size
        {
            get => Rect.Size;
            set => Rect.Size = value;
        }

        public Point Position
        {
            get => Rect.Position;
            set => Rect.Position = value;
        }

        public Rotator Rotation
        {
            get => Rect.Rotation;
            set => Rect.Rotation = value;
        }

        public Point Scale
        {
            get => Rect.Scale;
            set => Rect.Scale = value;
        }

        private readonly List<Viewport> _connections = [];

        internal static Viewport Active { get; set; } = WindowComponentManager.WindowViewport;

        internal void Use()
        {
            Active = this;
            GL.Viewport((int)Position.X, (int)(Window.Size.Y - Position.Y - Size.Y), (int)Size.X, (int)Size.Y);
        }

        public override string ToString()
        {
            return Rect.ToString();
        }
    }
}
