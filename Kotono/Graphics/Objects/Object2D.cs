﻿
using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal abstract class Object2D<T> : Drawable<T>, IObject2D where T : Object2DSettings
    {
        private Rect _rect = Rect.Default;

        public virtual Rect Rect
        {
            get => _rect;
            set
            {
                if (!ReferenceEquals(_rect, value))
                {
                    _rect.Dispose();
                    _rect = value;
                }
            }
        }

        public Point BaseSize
        {
            get => _rect.BaseSize;
            set => _rect.BaseSize = value;
        }

        public virtual Point Size
        {
            get => _rect.Size;
            set => _rect.Size = value;
        }

        public virtual Point Position
        {
            get => _rect.Position;
            set => _rect.Position = value;
        }

        public virtual Rotator Rotation
        {
            get => _rect.Rotation;
            set => _rect.Rotation = value;
        }

        public virtual Point Scale
        {
            get => _rect.Scale;
            set => _rect.Scale = value;
        }

        public virtual int Layer
        {
            get => _settings.Layer;
            set => _settings.Layer = value;
        }

        public override bool IsHovered => Rect.Overlaps(Rect, Mouse.Position);

        public new IObject2D? Parent { get; set; }

        internal Object2D(T settings)
            : base(settings)
        {
            Rect = settings.Rect;
        }

        internal Object2D() : base() { }

        public override void Save()
        {
            _settings.Rect = Rect;

            base.Save();
        }

        public override void Dispose()
        {
            Rect.Dispose();

            base.Dispose();
        }
    }
}
