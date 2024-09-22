using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal sealed class Viewport : IRect
    {
        internal static Viewport Active { get; set; } = WindowComponentManager.WindowViewport;

        internal List<Viewport> Connections { get; } = [];

        internal Rect Rect { get; } = Rect.Default;

        public Anchor Anchor
        {
            get => Rect.Anchor;
            set => Rect.Anchor = value;
        }

        public Point BaseSize
        {
            get => Rect.BaseSize;
            set => Rect.BaseSize = value;
        }

        public Point RelativeSize
        {
            get => Rect.RelativeSize;
            set => Rect.RelativeSize = value;
        }

        public Point RelativePosition
        {
            get => Rect.RelativePosition;
            set => Rect.RelativePosition = value;
        }

        public Rotator RelativeRotation
        {
            get => Rect.RelativeRotation;
            set => Rect.RelativeRotation = value;
        }

        public Point RelativeScale
        {
            get => Rect.RelativeScale;
            set => Rect.RelativeScale = value;
        }

        public Point WorldSize
        {
            get => Rect.WorldSize;
            set => Rect.WorldSize = value;
        }

        public Point WorldPosition
        {
            get => Rect.WorldPosition;
            set => Rect.WorldPosition = value;
        }

        public Rotator WorldRotation
        {
            get => Rect.WorldRotation;
            set => Rect.WorldRotation = value;
        }

        public Point WorldScale
        {
            get => Rect.WorldScale;
            set => Rect.WorldScale = value;
        }

        internal void Use()
        {
            Active = this;
            GL.Viewport((int)RelativePosition.X, (int)(Window.Size.Y - RelativePosition.Y - RelativeSize.Y), (int)RelativeSize.X, (int)RelativeSize.Y);
        }

        public override string ToString() => Rect.ToString();
    }
}
