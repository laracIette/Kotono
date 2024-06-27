using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Viewport(Rect rect) : IRect
    {
        private readonly Rect _rect = rect;

        public Point BaseSize
        {
            get => _rect.BaseSize;
            set => _rect.BaseSize = value;
        }

        public Point Size
        {
            get => _rect.Size;
            set => _rect.Size = value;
        }

        public Point Position
        {
            get => _rect.Position;
            set => _rect.Position = value;
        }

        public Rotator Rotation
        {
            get => _rect.Rotation;
            set => _rect.Rotation = value;
        }

        public Point Scale
        {
            get => _rect.Scale;
            set => _rect.Scale = value;
        }

        private readonly List<Viewport> _connections = [];

        internal static Viewport Active { get; set; } = WindowComponentManager.WindowViewport;

        internal void Use()
        {
            Active = this;
            GL.Viewport((int)Position.X, (int)(Window.Size.Y - Position.Y - Size.Y), (int)Size.X, (int)Size.Y);
        }
    }
}
