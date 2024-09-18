using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;

namespace Kotono.Utils.Coordinates
{
    internal sealed partial class Rect : Object, IRect, IEquatable<Rect>
    {
        private sealed record class Transformation<T>(T Value, float EndTime) where T : struct;

        private Transformation<Point>? _positionTransformation = null;

        private Transformation<Rotator>? _rotationTransformation = null;

        private Transformation<Point>? _sizeTransformation = null;

        private Point _relativePosition = DefaultPosition;

        public Anchor Anchor { get; set; } = Anchor.Center;

        private Point AnchorDelta => new(
              (Anchor & Anchor.Left) == Anchor.Left ? Math.Half(RelativeSize.X)
            : (Anchor & Anchor.Right) == Anchor.Right ? -Math.Half(RelativeSize.X)
            : 0.0f,
              (Anchor & Anchor.Top) == Anchor.Top ? Math.Half(RelativeSize.Y)
            : (Anchor & Anchor.Bottom) == Anchor.Bottom ? -Math.Half(RelativeSize.Y)
            : 0.0f
        );

        public Point RelativePosition
        {
            get => _relativePosition + AnchorDelta;
            set => _relativePosition = value;
        }

        public Point BaseSize { get; set; } = DefaultSize;

        public Point RelativeSize
        {
            get => BaseSize * RelativeScale;
            set
            {
                if (Point.IsZero(BaseSize))
                {
                    BaseSize = value / RelativeScale;
                }
                else
                {
                    RelativeScale = value / BaseSize;
                }
            }
        }

        public Rotator RelativeRotation { get; set; } = DefaultRotation;

        public Point RelativeScale { get; set; } = DefaultScale;

        public Point WorldPosition
        {
            get => RelativePosition + ParentWorldPosition;
            set => RelativePosition = value - ParentWorldPosition;
        }

        public Point WorldSize
        {
            get => BaseSize * WorldScale;
            set
            {
                if (Point.IsZero(BaseSize))
                {
                    BaseSize = value / WorldScale;
                }
                else
                {
                    WorldScale = value / BaseSize;
                }
            }
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

        private Point ParentWorldPosition => Parent?.WorldPosition ?? DefaultPosition;

        private Point ParentWorldSize => Parent?.WorldSize ?? DefaultSize;

        private Rotator ParentWorldRotation => Parent?.WorldRotation ?? DefaultRotation;

        private Point ParentWorldScale => Parent?.WorldScale ?? DefaultScale;

        /// <summary>
        /// The Normalized Device Coordinates of the Rect.
        /// </summary>
        internal NDCRect NDC => new(WorldPosition, WorldSize);

        /// <summary>
        /// The model matrix of the Rect.
        /// </summary>
        internal Matrix4 Model => NDC.Model;

        /// <summary>
        /// The center Point of the Rect.
        /// </summary>
        internal Point Center => RelativePosition;

        /// <summary>
        /// The left Point of the Rect.
        /// </summary>
        internal Point Left => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y);

        /// <summary>
        /// The right Point of the Rect.
        /// </summary>
        internal Point Right => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y);

        /// <summary>
        /// The top Point of the Rect.
        /// </summary>
        internal Point Top => new(RelativePosition.X, RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom Point of the Rect.
        /// </summary>
        internal Point Bottom => new(RelativePosition.X, RelativePosition.Y - Math.Half(RelativeSize.Y));

        /// <summary>
        /// The top left Point of the Rect.
        /// </summary>
        internal Point TopLeft => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The top right Point of the Rect.
        /// </summary>
        internal Point TopRight => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y + Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom left Point of the Rect.
        /// </summary>
        internal Point BottomLeft => new(RelativePosition.X - Math.Half(RelativeSize.X), RelativePosition.Y - Math.Half(RelativeSize.Y));

        /// <summary>
        /// The bottom right Point of the Rect.
        /// </summary>
        internal Point BottomRight => new(RelativePosition.X + Math.Half(RelativeSize.X), RelativePosition.Y - Math.Half(RelativeSize.Y));

        internal static Point DefaultPosition => Point.Zero;

        internal static Point DefaultSize => Point.Zero;

        internal static Rotator DefaultRotation => Rotator.Zero;

        internal static Point DefaultScale => Point.Unit;

        /// <summary> 
        /// A Rect with 
        /// Position = <see cref="Point.Zero"/>,
        /// Size = <see cref="Point.Zero"/>, 
        /// Rotation = <see cref="Rotator.Zero"/>,
        /// Scale = <see cref="Point.Unit"/>.
        /// </summary>
        internal static Rect Default => new(DefaultPosition, DefaultSize, DefaultRotation);

        internal Rect() : this(DefaultPosition, DefaultSize, DefaultRotation) { }

        internal Rect(Point position, Point baseSize, Point size, Rotator rotation)
        {
            RelativePosition = position;
            BaseSize = baseSize;
            RelativeSize = size;
            RelativeRotation = rotation;
        }

        internal Rect(Point position, Point baseSize, Rotator rotation, Point scale)
        {
            RelativePosition = position;
            BaseSize = baseSize;
            RelativeRotation = rotation;
            RelativeScale = scale;
        }

        internal Rect(Point position, Point size, Rotator rotation)
        {
            RelativePosition = position;
            BaseSize = size;
            RelativeRotation = rotation;
        }

        internal Rect(Point position, Point size)
        {
            RelativePosition = position;
            BaseSize = size;
        }

        public override void Update()
        {
            //RelativePosition += Time.Delta * RelativePositionVelocity;
            //RelativeRotation += Time.Delta * RelativeRotationVelocity;
            //RelativeSize += Time.Delta * RelativeSizeVelocity;

            if (_positionTransformation is not null
             && TryGetTransformation(ref _positionTransformation, out var position))
            {
                Logger.Log(position);
                RelativePosition += Time.Delta * position;
            }

            if (_rotationTransformation is not null
             && TryGetTransformation(ref _rotationTransformation, out var rotation))
            {
                RelativeRotation += Time.Delta * rotation;
            }

            if (_sizeTransformation is not null
             && TryGetTransformation(ref _sizeTransformation, out var size))
            {
                RelativeSize += Time.Delta * size;
            }
        }

        private static bool TryGetTransformation<T>(ref Transformation<T>? transformation, out T value) where T : struct
        {
            if (transformation!.EndTime >= Time.Now)
            {
                value = transformation.Value;
                return true;
            }
            else
            {
                value = default;
                transformation = null;
                return false;
            }
        }

        /// <summary>
        /// Transform the <see cref="Rect"/>'s position in a given time span.
        /// </summary>
        internal void SetPositionTransformation(Point position, float duration)
        {
            if (duration <= 0.0f)
            {
                RelativePosition += position;
            }
            else
            {
                _positionTransformation = new(position / duration, Time.Now + duration);
            }
        }

        /// <summary>
        /// Transform the <see cref="Rect"/>'s rotation in a given time span.
        /// </summary>
        internal void SetRotationTransformation(Rotator rotation, float duration)
        {
            if (duration <= 0.0f)
            {
                RelativeRotation += rotation;
            }
            else
            {
                _rotationTransformation = new(rotation / duration, Time.Now + duration);
            }
        }

        /// <summary>
        /// Transform the <see cref="Rect"/>'s size in a given time span.
        /// </summary>
        internal void SetSizeTransformation(Point size, float duration)
        {
            if (duration <= 0.0f)
            {
                RelativeSize += size;
            }
            else
            {
                _sizeTransformation = new(size / duration, Time.Now + duration);
            }
        }

        internal static Point GetPositionFromAnchor(Point position, Point size, Anchor anchor, Point offset)
        {
            var (x, y) = position + offset;

            if ((anchor & Anchor.Left) == Anchor.Left)
            {
                x += Math.Half(size.X);
            }
            else if ((anchor & Anchor.Right) == Anchor.Right)
            {
                x -= Math.Half(size.X);
            }

            if ((anchor & Anchor.Top) == Anchor.Top)
            {
                y += Math.Half(size.Y);
            }
            else if ((anchor & Anchor.Bottom) == Anchor.Bottom)
            {
                y -= Math.Half(size.Y);
            }

            return new Point(x, y);
        }

        /// <summary> 
        /// Get the position given a position, a size and an Anchor.
        /// </summary>
        internal static Point GetPositionFromAnchor(Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            return GetPositionFromAnchor(position, size, anchor, new Point(offset));
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        internal static void GetPositionsFromAnchor(Point[] points, Point position, Point size, Anchor anchor, Point offset)
        {
            for (int i = 0; i < points.Length; i++)
            {
                var (x, y) = GetPositionFromAnchor(position, size, anchor, offset);

                y = anchor switch
                {
                    Anchor.Center or Anchor.Left or Anchor.Right => y - Math.Half(size.Y) * (points.Length - 1) + size.Y * i,
                    Anchor.Top or Anchor.TopLeft or Anchor.TopRight => throw new NotImplementedException(),
                    Anchor.Bottom or Anchor.BottomLeft or Anchor.BottomRight => throw new NotImplementedException(),
                    _ => throw new SwitchException(typeof(Anchor), anchor)
                };

                points[i] = new Point(x, y);
            }
        }

        /// <summary>
        /// Creates an array of Rect given a number of elements, a Rect and an Anchor.
        /// </summary>
        internal static void GetPositionsFromAnchor(Point[] points, Point position, Point size, Anchor anchor, float offset = 0.0f)
        {
            GetPositionsFromAnchor(points, position, size, anchor, new Point(offset));
        }

        /// <summary> 
        /// Checks if left is overlapping with right.
        /// </summary>
        internal static bool Overlaps(IRect left, IRect right)
        {
            return Point.Abs(left.WorldPosition - right.WorldPosition) < Point.Half(left.WorldSize + right.WorldSize);
        }

        /// <summary> 
        /// Checks if the <see cref="Rect"/> is overlapping with p.
        /// </summary>
        internal bool Overlaps(Point p)
        {
            return Point.Abs(RelativePosition - p) < Point.Half(RelativeSize);
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
            return r is not null
                && r.WorldPosition == WorldPosition
                && r.WorldRotation == WorldRotation
                && r.WorldSize == WorldSize;
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
            return $"Relative: {{Position: {{{RelativePosition}}}, Rotation: {{{RelativeRotation}}}, Size: {{{RelativeSize}}}}} "
                 + $"World: {{Position: {{{WorldPosition}}}, Rotation: {{{WorldRotation}}}, Size: {{{WorldSize}}}}}";
        }

        internal string ToString(CoordinateSpace coordinateSpace)
        {
            return coordinateSpace switch
            {
                CoordinateSpace.Relative => $"Relative: {{Position: {{{RelativePosition}}}, Rotation: {{{RelativeRotation}}}, Size: {{{RelativeSize}}}}}",
                CoordinateSpace.World => $"World: {{Position: {{{WorldPosition}}}, Rotation: {{{WorldRotation}}}, Size: {{{WorldSize}}}}}",
                _ => throw new SwitchException(typeof(CoordinateSpace), coordinateSpace)
            };
        }
    }
}
