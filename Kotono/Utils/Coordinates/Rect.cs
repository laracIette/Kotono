using Kotono.Graphics.Objects;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    internal class Rect : Object, IRect, IReplaceable<Rect>, IEquatable<Rect>
    {
        private record class Transformation(RectBase RectBase, float EndTime);

        private Transformation? _transformation = null;

        private RectBase _base = new(Point.Zero, Point.Zero, Point.Unit);

        public Point BaseSize 
        {
            get => _base.BaseSize;
            set => _base.BaseSize = value;
        }

        public Point Size => _base.Size;

        public Point Position 
        { 
            get => _base.Position; 
            set => _base.Position = value; 
        }

        public Point Scale
        {
            get => _base.Scale;
            set => _base.Scale = value;
        }

        /// <summary>
        /// The Rect scaled to Normalized Device Coordinates.
        /// </summary>
        public NDCRect NDC => new(Position, Size);

        /// <summary>
        /// The model matrix of the Rect.
        /// </summary>
        public Matrix4 Model =>
            Matrix4.CreateScale(NDC.Size.X, NDC.Size.Y, 1.0f)
            * Matrix4.CreateTranslation(NDC.Position.X, NDC.Position.Y, 0.0f);

        /// <summary>
        /// The center Point of the Rect.
        /// </summary>
        public Point Center => Position;

        /// <summary>
        /// The left Point of the Rect.
        /// </summary>
        public Point Left => new(Position.X - Size.X / 2.0f, Position.Y);

        /// <summary>
        /// The right Point of the Rect.
        /// </summary>
        public Point Right => new(Position.X + Size.X / 2.0f, Position.Y);

        /// <summary>
        /// The top Point of the Rect.
        /// </summary>
        public Point Top => new(Position.X, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The bottom Point of the Rect.
        /// </summary>
        public Point Bottom => new(Position.X, Position.Y - Size.Y / 2.0f);

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        public Point TopLeft => new(Position.X - Size.X / 2.0f, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The top right Point of the Rect.
        /// </summary>
        public Point TopRight => new(Position.X + Size.X / 2.0f, Position.Y + Size.Y / 2.0f);

        /// <summary>
        /// The bottom left Point of the Rect.
        /// </summary>
        public Point BottomLeft => new(Position.X - Size.X / 2.0f, Position.Y - Size.Y / 2.0f);

        /// <summary>
        /// The bottom right Point of the Rect.
        /// </summary>
        public Point BottomRight => new(Position.X + Size.X / 2.0f, Position.Y - Size.Y / 2.0f);

        /// <summary> 
        /// A Rect with X = 0, Y = 0, W = 0, H = 0.
        /// </summary>
        public static Rect Default => new(0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f);

        public Rect(float x = 0.0f, float y = 0.0f, float w = 0.0f, float h = 0.0f, float sx = 1.0f, float sy = 1.0f)
            : base()
        {
            _base = new RectBase(new Point(x, y), new Point(w, h), new Point(sx, sy));
        }

        [JsonConstructor]
        public Rect() : this(0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f) { }

        public Rect(Rect r) : this(r.Position.X, r.Position.Y, r.BaseSize.X, r.BaseSize.Y, r.Scale.X, r.Scale.Y) { }

        public Rect(Point position, Point size) : this(position.X, position.Y, size.X, size.Y) { }
        
        public Rect(Point position, Point size, Point scale) : this(position.X, position.Y, size.X, size.Y, scale.X, scale.Y) { }

        public Rect(Point position, float w, float h) : this(position.X, position.Y, w, h) { }

        public Rect(float x, float y, Point size) : this(x, y, size.X, size.Y) { }

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

        public static Point FromAnchor(Point position, Point size, Anchor anchor, Point offset)
        {
            position += anchor switch
            {
                Anchor.Center => offset,
                Anchor.Left => new Point(size.X / 2.0f + offset.X, offset.Y),
                Anchor.Right => new Point(-size.X / 2.0f - offset.X, offset.Y),
                Anchor.Top => new Point(offset.X, size.Y / 2.0f + offset.Y),
                Anchor.Bottom => new Point(offset.X, -size.Y / 2.0f - offset.Y),
                Anchor.TopLeft => new Point(size.X / 2.0f + offset.X, size.Y / 2.0f + offset.Y),
                Anchor.TopRight => new Point(-size.X / 2.0f - offset.X, size.Y / 2.0f + offset.Y),
                Anchor.BottomLeft => new Point(size.X / 2.0f + offset.X, -size.Y / 2.0f - offset.Y),
                Anchor.BottomRight => new Point(-size.X / 2.0f - offset.X, -size.Y / 2.0f - offset.Y),
                _ => throw new Exception($"error: Rect.FromAnchor() doesn't handle \"{anchor}\"")
            };

            return position;
        }

        /// <summary> 
        /// Creates a Rect given a Rect and an Anchor.
        /// </summary>
        public static Point FromAnchor(Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            return FromAnchor(position, size, anchor, new Point(offset));
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        public static Point[] FromAnchor(int n, Point position, Point size, Anchor anchor, Point offset)
        {
            var result = Enumerable.Repeat(FromAnchor(position, size, anchor, offset), n).ToArray();

            for (int i = 0; i < n; i++)
            {
                result[i].Y = anchor switch
                {
                    Anchor.Center or Anchor.Left or Anchor.Right => position.Y - size.Y / 2.0f * (n - 1) + size.Y * i,
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
        public static Point[] FromAnchor(int n, Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            return FromAnchor(n, position, size, anchor, new Point(offset));
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        public static bool Overlaps(RectBase left, RectBase right)
        {
            return Math.Abs(left.Position.X - right.Position.X) < (left.Size.X + right.BaseSize.X) / 2.0f
                && Math.Abs(left.Position.Y - right.Position.Y) < (left.Size.Y + right.BaseSize.Y) / 2.0f;
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
            return Math.Abs(r.Position.X - p.X) < r.Size.X / 2.0f
                && Math.Abs(r.Position.Y - p.Y) < r.Size.Y / 2.0f;
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
            return ReferenceEquals(this, r);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Size);
        }

        public static explicit operator Vector4(Rect r)
        {
            return new Vector4(r.Position.X, r.Position.Y, r.Size.X, r.Size.Y);
        }

        public static explicit operator Rect(Vector4 v)
        {
            return new Rect(v.X, v.Y, v.Z, v.W);
        }

        public override string ToString()
        {
            return $"X: {Position.X}, Y: {Position.Y}, W: {Size.X}, H: {Size.Y}"; ;
        }

        public void ReplaceBy(Rect r)
        {
            _base = r._base;
        }
    }
}
