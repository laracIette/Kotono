using Kotono.Graphics;
using Kotono.Graphics.Objects;
using OpenTK.Mathematics;
using System;

namespace Kotono.Utils.Coordinates
{
    internal sealed class Transform : Object, ITransform, IEquatable<Transform>
    {
        private sealed class Base
        {
            public Vector Location { get; set; } = DefaultLocation;

            public Rotator Rotation { get; set; } = DefaultRotation;

            public Vector Scale { get; set; } = DefaultScale;
        }

        private sealed record class Transformation<T>(T Value, float EndTime) where T : struct;

        private Transformation<Vector>? _locationTransformation = null;

        private Transformation<Rotator>? _rotationTransformation = null;

        private Transformation<Vector>? _scaleTransformation = null;

        private readonly Base _base = new();

        private readonly Base _velocity = new();

        public Vector RelativeLocation
        {
            get => _base.Location;
            set => _base.Location = value;
        }

        public Rotator RelativeRotation
        {
            get => _base.Rotation;
            set => _base.Rotation = value;
        }

        public Vector RelativeScale
        {
            get => _base.Scale;
            set => _base.Scale = value;
        }

        public Vector RelativeLocationVelocity
        {
            get => _velocity.Location;
            set => _velocity.Location = value;
        }

        public Rotator RelativeRotationVelocity
        {
            get => _velocity.Rotation;
            set => _velocity.Rotation = value;
        }

        public Vector RelativeScaleVelocity
        {
            get => _velocity.Scale;
            set => _velocity.Scale = value;
        }

        public Vector WorldLocation
        {
            get => RelativeLocation + ParentWorldLocation;
            set => RelativeLocation = value - ParentWorldLocation;
        }

        public Rotator WorldRotation
        {
            get => RelativeRotation + ParentWorldRotation;
            set => RelativeRotation = value - ParentWorldRotation;
        }

        public Vector WorldScale
        {
            get => RelativeScale * ParentWorldScale;
            set => RelativeScale = value / ParentWorldScale;
        }

        public Vector WorldLocationVelocity
        {
            get => RelativeLocationVelocity + ParentWorldLocationVelocity;
            set => RelativeLocationVelocity = value - ParentWorldLocationVelocity;
        }

        public Rotator WorldRotationVelocity
        {
            get => RelativeRotationVelocity + ParentWorldRotationVelocity;
            set => RelativeRotationVelocity = value - ParentWorldRotationVelocity;
        }

        public Vector WorldScaleVelocity
        {
            get => RelativeScaleVelocity * ParentWorldScaleVelocity;
            set => RelativeScaleVelocity = value / ParentWorldScaleVelocity;
        }

        /// <summary>
        /// The <see cref="Transform"/> the <see cref="Transform"/> is relative to.
        /// </summary>
        public Transform? Parent { get; set; } = null;

        private Vector ParentWorldLocation => Parent?.WorldLocation ?? DefaultLocation;

        private Rotator ParentWorldRotation => Parent?.WorldRotation ?? DefaultRotation;

        private Vector ParentWorldScale => Parent?.WorldScale ?? DefaultScale;

        private Vector ParentWorldLocationVelocity => Parent?.WorldLocationVelocity ?? DefaultLocation;

        private Rotator ParentWorldRotationVelocity => Parent?.WorldRotationVelocity ?? DefaultRotation;

        private Vector ParentWorldScaleVelocity => Parent?.WorldScaleVelocity ?? DefaultScale;

        /// <summary> 
        /// The right vector of the Transform. 
        /// </summary>
        internal Vector Right => (Vector)(Quaternion.FromEulerAngles((Vector3)RelativeRotation) * Vector3.UnitX);

        /// <summary> 
        /// The up vector of the Transform. 
        /// </summary>
        internal Vector Up => (Vector)(Quaternion.FromEulerAngles((Vector3)RelativeRotation) * Vector3.UnitY);

        /// <summary> 
        /// The forward vector of the Transform. 
        /// </summary>
        internal Vector Forward => (Vector)(Quaternion.FromEulerAngles((Vector3)RelativeRotation) * Vector3.UnitZ);

        /// <summary>
        /// The model matrix of the Transform.
        /// </summary>
        internal Matrix4 Model
            => Vector.CreateScaleMatrix(WorldScale)
            * WorldRotation.RotationMatrix
            * Vector.CreateTranslationMatrix(WorldLocation);

        /// <summary>
        /// The position on screen of the location of the <see cref="Transform"/>.
        /// </summary>
        internal Point ScreenPosition
        {
            get
            {
                // Compute the MVP matrix
                Matrix4 mvpMatrix = Model * Camera.Active.ViewMatrix * Camera.Active.ProjectionMatrix;

                // Convert the world position to clip space
                Vector4 clipSpacePos = new Vector4(WorldLocation.X, WorldLocation.Y, WorldLocation.Z, 1.0f);
                clipSpacePos = Vector4.TransformRow(clipSpacePos, mvpMatrix);

                // Perform perspective division to transform to normalized device coordinates (NDC)
                Vector3 ndcPos = new Vector3(clipSpacePos.X, clipSpacePos.Y, clipSpacePos.Z) / clipSpacePos.W;

                // Convert NDC to screen space
                float screenX = (ndcPos.X + 1.0f) * 0.5f * Viewport.Active.RelativeSize.X;
                float screenY = (1.0f - ndcPos.Y) * 0.5f * Viewport.Active.RelativeSize.Y; // Y is inverted for screen space

                return new Point(screenX, screenY);
            }
        }

        /// <summary> 
        /// A Transform with default location, rotation and scale. 
        /// </summary>
        internal static Transform Default => new(DefaultLocation, DefaultRotation, DefaultScale);

        /// <summary>
        /// The default location of Transform.
        /// </summary>
        /// <remarks>
        /// Value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultLocation => Vector.Zero;

        /// <summary>
        /// The default rotation of Transform.
        /// </summary>
        /// <remarks>
        /// Value : Rotator.Zero
        /// </remarks>
        internal static Rotator DefaultRotation => Rotator.Zero;

        /// <summary>
        /// The default scale of Transform.
        /// </summary>
        /// <remarks>
        /// Value : Vector.Unit
        /// </remarks>
        internal static Vector DefaultScale => Vector.Unit;

        /// <summary>
        /// The default velocity of Transform's location.
        /// </summary>
        /// <remarks>
        /// Value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultLocationVelocity => Vector.Zero;

        /// <summary>
        /// The default velocity of Transform's rotation.
        /// </summary>
        /// <remarks>
        /// Value : Rotator.Zero
        /// </remarks>
        internal static Rotator DefaultRotationVelocity => Rotator.Zero;

        /// <summary>
        /// The default velocity of Transform's scale.
        /// </summary>
        /// <remarks>
        /// Value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultScaleVelocity => Vector.Zero;

        internal Transform(Vector location, Rotator rotation, Vector scale)
        {
            RelativeLocation = location;
            RelativeRotation = rotation;
            RelativeScale = scale;
        }

        internal Transform() : this(DefaultLocation, DefaultRotation, DefaultScale) { }

        public override void Update()
        {
            RelativeLocation += Time.Delta * RelativeLocationVelocity;
            RelativeRotation += Time.Delta * RelativeRotationVelocity;
            RelativeScale += Time.Delta * RelativeScaleVelocity;

            if (TryGetTransformation(ref _locationTransformation, out var location))
            {
                RelativeLocation += Time.Delta * location;
            }

            if (TryGetTransformation(ref _rotationTransformation, out var rotation))
            {
                RelativeRotation += Time.Delta * rotation;
            }

            if (TryGetTransformation(ref _scaleTransformation, out var scale))
            {
                RelativeScale += Time.Delta * scale;
            }
        }

        private static bool TryGetTransformation<T>(ref Transformation<T>? transformation, out T value) where T : struct
        {
            if (transformation?.EndTime >= Time.Now)
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
        /// Transform the <see cref="Transform"/>'s location in a given time span.
        /// </summary>
        /// <param name="t"> The location to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
        internal void SetLocationTransformation(Vector location, float duration)
        {
            if (duration <= 0.0f)
            {
                RelativeLocation += location;
            }
            else
            {
                _locationTransformation = new(location / duration, Time.Now + duration);
            }
        }

        /// <summary>
        /// Transform the <see cref="Transform"/>'s rotation in a given time span.
        /// </summary>
        /// <param name="t"> The rotation to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
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
        /// Transform the <see cref="Transform"/>'s scale in a given time span.
        /// </summary>
        /// <param name="t"> The scale to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
        internal void SetScaleTransformation(Vector scale, float duration)
        {
            if (duration <= 0.0f)
            {
                RelativeScale += scale;
            }
            else
            {
                _scaleTransformation = new(scale / duration, Time.Now + duration);
            }
        }

        public static bool operator ==(Transform? left, Transform? right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(Transform? left, Transform? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Transform t && Equals(t);
        }

        public bool Equals(Transform? t)
        {
            return t is not null
                && t.WorldLocation == WorldLocation
                && t.WorldRotation == WorldRotation
                && t.WorldScale == WorldScale;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WorldLocation, WorldRotation, WorldScale);
        }

        public override string ToString()
        {
            return $"World: {{Location: {WorldLocation}, Rotation: {WorldRotation}, Scale: {WorldScale}}}\n"
                 + $"Relative: {{Location: {RelativeLocation}, Rotation: {RelativeRotation}, Scale: {RelativeScale}}}";
        }
    }
}