using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Linq;

namespace Kotono.Utils.Coordinates
{
    internal class Rect : Object, IRect, IEquatable<Rect>
    {
        private record class Transformation(RectBase RectBase, float EndTime);

        private Transformation? _transformation = null;

        private RectBase _base = new(DefaultPosition, DefaultSize);

        public Point BaseSize
        {
            get => _base.BaseSize;
            set => _base.BaseSize = value;
        }

        public Point RelativeSize
        {
            get => _base.Size;
            set => _base.Size = value;
        }

        public Point RelativePosition
        {
            get => _base.Position;
            set => _base.Position = value;
        }

        public Rotator RelativeRotation
        {
            get => _base.Rotation;
            set => _base.Rotation = value;
        }

        public Point RelativeScale
        {
            get => _base.Scale;
            set => _base.Scale = value;
        }

        public Point WorldSize
        {
            get => RelativeSize + ParentWorldSize;
            set => RelativeSize = value - ParentWorldSize;
        }

        public Point WorldPosition
        {
            get => RelativePosition + ParentWorldPosition;
            set => RelativePosition = value - ParentWorldPosition;
        }

        public Rotator WorldRotation
        {
            get => RelativeRotation + ParentWorldRotation;
            set => RelativeRotation = value - ParentWorldRotation;
        }

        public Point WorldScale
        {
            get => RelativeScale * ParentWorldScale;
            set => RelativeScale = value / ParentWorldScale;
        }

        /// <summary>
        /// The <see cref="Rect"/> the <see cref="Rect"/> is relative to.
        /// </summary>
        public Rect? Parent { get; set; } = null;

        private Point ParentWorldSize => Parent?.WorldSize ?? DefaultSize;

        private Point ParentWorldPosition => Parent?.WorldPosition ?? DefaultPosition;

        private Rotator ParentWorldRotation => Parent?.WorldRotation ?? DefaultRotation;

        private Point ParentWorldScale => Parent?.WorldScale ?? DefaultScale;

        /// <summary>
        /// The Normalized Device Coordinates of the Rect.
        /// </summary>
        public NDCRect NDC => new(WorldPosition, WorldSize);

        /// <summary>
        /// The model matrix of the Rect.
        /// </summary>
        public Matrix4 Model => NDC.Model;

        /// <summary>
        /// The center Point of the Rect.
        /// </summary>
        public Point Center => RelativePosition;

        /// <summary>
        /// The left Point of the Rect.
        /// </summary>
        public Point Left => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y);

        /// <summary>
        /// The right Point of the Rect.
        /// </summary>
        public Point Right => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y);

        /// <summary>
        /// The top Point of the Rect.
        /// </summary>
        public Point Top => new(RelativePosition.X, RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom Point of the Rect.
        /// </summary>
        public Point Bottom => new(RelativePosition.X, RelativePosition.Y - Math.Half(RelativeSize.Y));

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        public Point TopLeft => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The top right Point of the Rect.
        /// </summary>
        public Point TopRight => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom left Point of the Rect.
        /// </summary>
        public Point BottomLeft => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y - Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom right Point of the Rect.
        /// </summary>
        public Point BottomRight => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y - Math.Half(RelativeSize.Y));

        public static Point DefaultPosition => Point.Zero;

        public static Point DefaultSize => Point.Zero;

        public static Rotator DefaultRotation => Rotator.Zero;

        public static Point DefaultScale => Point.Unit;

        /// <summary> 
        /// A Rect with 
        /// Position = <see cref="Point.Zero"/>,
        /// Size = <see cref="Point.Zero"/>, 
        /// Rotation = <see cref="Rotator.Zero"/>,
        /// Scale = <see cref="Point.Unit"/>.
        /// </summary>
        public static Rect Default => new(DefaultPosition, DefaultSize, DefaultRotation);

        public Rect() : this(DefaultPosition, DefaultSize, DefaultRotation) { }

        public Rect(Point position, Point baseSize, Point size, Rotator rotation)
        {
            _base = new RectBase(position, baseSize, size, rotation);
        }

        public Rect(Point position, Point baseSize, Rotator rotation, Point scale)
        {
            _base = new RectBase(position, baseSize, rotation, scale);
        }

        public Rect(Point position, Point size, Rotator rotation)
        {
            _base = new RectBase(position, size, rotation);
        }

        public Rect(Point position, Point size)
        {
            _base = new RectBase(position, size);
        }

        public override void Update()
        {
            if (_transformation != null)
            {
                if (Time.Now > _transformation.EndTime)
                {
                    _transformation = null;
                }
                else
                {
                    _base += Time.Delta * _transformation.RectBase;
                }
            }
        }

        /// <summary>
        /// Transform the rect of the <see cref="Image"/> in a given time span.
        /// </summary>
        /// <param name="r"> The transformation to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
        internal void SetTransformation(RectBase r, float duration)
        {
            if (duration <= 0.0f)
            {
                _base = r;
            }
            else
            {
                _transformation = new Transformation(r / duration, Time.Now + duration);
            }
        }

        public static Point GetPositionFromAnchor(Point position, Point size, Anchor anchor, Point offset)
        {
            if ((anchor & Anchor.Left) == Anchor.Left)
            {
                position.X += Math.Half(size.X) + offset.X;
            }
            else if ((anchor & Anchor.Right) == Anchor.Right)
            {
                position.X -= Math.Half(size.X) + offset.X;
            }
            else // Centered horizontally
            {
                position.X += offset.X;
            }

            if ((anchor & Anchor.Top) == Anchor.Top)
            {
                position.Y += Math.Half(size.Y) + offset.Y;
            }
            else if ((anchor & Anchor.Bottom) == Anchor.Bottom)
            {
                position.Y -= Math.Half(size.Y) + offset.Y;
            }
            else // Centered vertically
            {
                position.Y += offset.Y;
            }

            return position;
        }

        /// <summary> 
        /// Get the position given a position, a size and an Anchor.
        /// </summary>
        public static Point GetPositionFromAnchor(Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            return GetPositionFromAnchor(position, size, anchor, new Point(offset));
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        public static Point[] GetPositionFromAnchor(int n, Point position, Point size, Anchor anchor, Point offset)
        {
            var result = Enumerable.Repeat(GetPositionFromAnchor(position, size, anchor, offset), n).ToArray();

            for (int i = 0; i < n; i++)
            {
                result[i].Y = anchor switch
                {
                    Anchor.Center or Anchor.Left or Anchor.Right => position.Y - Math.Half(size.Y) * (n - 1) + size.Y * i,
                    Anchor.Top or Anchor.TopLeft or Anchor.TopRight => throw new NotImplementedException(),
                    Anchor.Bottom or Anchor.BottomLeft or Anchor.BottomRight => throw new NotImplementedException(),
                    _ => throw new SwitchException(typeof(Anchor), anchor)
                };
            }

            return result;
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        public static Point[] GetPositionFromAnchor(int n, Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            return GetPositionFromAnchor(n, position, size, anchor, new Point(offset));
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(RectBase left, RectBase right)
        {
            return Math.Abs(left.Position.X - right.Position.X) < Math.Half(left.Size.X + right.BaseSize.X)
                && Math.Abs(left.Position.Y - right.Position.Y) < Math.Half(left.Size.Y + right.BaseSize.Y);
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(Rect left, RectBase right)
        {
            return Overlaps(left._base, right);
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(Rect left, Rect right)
        {
            return Overlaps(left._base, right._base);
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        internal static bool Overlaps(IObject2D left, IObject2D right)
        {
            return Overlaps(left.Rect._base, right.Rect._base);
        }

        /// <summary> 
        /// Checks if r is overlapping with p.
        /// </summary>
        public static bool Overlaps(Rect r, Point p)
        {
            return Math.Abs(r.RelativePosition.X - p.X) < Math.Half(r.RelativeSize.X)
                && Math.Abs(r.RelativePosition.Y - p.Y) < Math.Half(r.RelativeSize.Y);
        }

        public static Rect Parse(string[] values)
        {
            return new Rect(
                (float.Parse(values[0]), float.Parse(values[1])),
                (float.Parse(values[2]), float.Parse(values[3]))
            );
        }

        public static bool operator ==(Rect? left, Rect? right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(Rect? left, Rect? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Rect r && Equals(r);
        }

        public bool Equals(Rect? r)
        {
            return r?._base == _base;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RelativePosition, RelativeSize);
        }

        public static explicit operator Vector4(Rect r)
        {
            return new Vector4(r.RelativePosition.X, r.RelativePosition.Y, r.RelativeSize.X, r.RelativeSize.Y);
        }

        public static explicit operator Rect(Vector4 v)
        {
            return new Rect(new Point(v.X, v.Y), new Point(v.Z, v.W));
        }

        public override string ToString()
        {
            return $"X: {RelativePosition.X}, Y: {RelativePosition.Y}, W: {RelativeSize.X}, H: {RelativeSize.Y}"; ;
        }
    }
}
