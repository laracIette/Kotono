using OpenTK.Mathematics;
using System;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    internal class Transform : Object, ITransform, IReplaceable<Transform>, ICloneable<Transform>, IEquatable<Transform>
    {
        private record class Transformation(TransformBase TransformBase, float EndTime);

        private Transformation? _transformation = null;

        private TransformBase _base = new(DefaultLocation, DefaultRotation, DefaultScale);
        
        private TransformBase _velocity = new(DefaultLocationVelocity, DefaultRotationVelocity, DefaultScaleVelocity);
        
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

        [JsonIgnore]
        public Vector WorldLocation
        {
            get => RelativeLocation + ParentWorldLocation;
            set => RelativeLocation = value - ParentWorldLocation;
        }

        [JsonIgnore]
        public Rotator WorldRotation
        {
            get => RelativeRotation + ParentWorldRotation;
            set => RelativeRotation = value - ParentWorldRotation;
        }

        [JsonIgnore]
        public Vector WorldScale
        {
            get => RelativeScale * ParentWorldScale;
            set => RelativeScale = value / ParentWorldScale;
        }

        [JsonIgnore]
        public Vector WorldLocationVelocity
        {
            get => RelativeLocationVelocity + ParentWorldLocationVelocity;
            set => RelativeLocationVelocity = value - ParentWorldLocationVelocity;
        }

        [JsonIgnore]
        public Rotator WorldRotationVelocity
        {
            get => RelativeRotationVelocity + ParentWorldRotationVelocity;
            set => RelativeRotationVelocity = value - ParentWorldRotationVelocity;
        }

        [JsonIgnore]
        public Vector WorldScaleVelocity
        {
            get => RelativeScaleVelocity * ParentWorldScaleVelocity;
            set => RelativeScaleVelocity = value / ParentWorldScaleVelocity;
        }

        /// <summary>
        /// The transform the Transform is relative to.
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
        internal Matrix4 Model =>
            Vector.CreateScaleMatrix(WorldScale)
            * WorldRotation.RotationMatrix
            * Vector.CreateTranslationMatrix(WorldLocation);

        /// <summary> 
        /// A Transform with default location, rotation and scale. 
        /// </summary>
        internal static Transform Default => new(DefaultLocation, DefaultRotation, DefaultScale);

        /// <summary>
        /// The default location of Transform.
        /// </summary>
        /// <remarks>
        /// Default value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultLocation => Vector.Zero;

        /// <summary>
        /// The default rotation of Transform.
        /// </summary>
        /// <remarks>
        /// Default value : Rotator.Zero
        /// </remarks>
        internal static Rotator DefaultRotation => Rotator.Zero;

        /// <summary>
        /// The default scale of Transform.
        /// </summary>
        /// <remarks>
        /// Default value : Vector.Unit
        /// </remarks>
        internal static Vector DefaultScale => Vector.Unit;

        /// <summary>
        /// The default velocity of Transform's location.
        /// </summary>
        /// <remarks>
        /// Default value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultLocationVelocity => Vector.Zero;

        /// <summary>
        /// The default velocity of Transform's rotation.
        /// </summary>
        /// <remarks>
        /// Default value : Rotator.Zero
        /// </remarks>
        internal static Rotator DefaultRotationVelocity => Rotator.Zero;

        /// <summary>
        /// The default velocity of Transform's scale.
        /// </summary>
        /// <remarks>
        /// Default value : Vector.Zero
        /// </remarks>
        internal static Vector DefaultScaleVelocity => Vector.Zero;

        internal Transform(Vector location, Rotator rotation, Vector scale)
            : base()
        {
            RelativeLocation = location;
            RelativeRotation = rotation;
            RelativeScale = scale;
        }

        [JsonConstructor]
        internal Transform() : this(DefaultLocation, DefaultRotation, DefaultScale) { }

        internal Transform(Transform t) : this(t.RelativeLocation, t.RelativeRotation, t.RelativeScale) { }

        public override void Update()
        {
            RelativeLocation += Time.Delta * RelativeLocationVelocity;
            RelativeRotation += Time.Delta * RelativeRotationVelocity;
            RelativeScale += Time.Delta * RelativeScaleVelocity;

            if (_transformation != null)
            {
                if (Time.Now > _transformation.EndTime)
                {
                    _transformation = null;
                }
                else
                {
                    _base += Time.Delta * _transformation.TransformBase;
                }
            }
        }

        /// <summary>
        /// Transform the <see cref="Transform"/> in a given time span.
        /// </summary>
        /// <param name="t"> The transformation to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
        internal void SetTransformation(TransformBase t, float duration)
        {
            if (duration <= 0.0f)
            {
                _base = t;
            }
            else
            {
                _transformation = new Transformation(t / duration, Time.Now + duration);
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
            return ReferenceEquals(this, t);
        }

        /// <inheritdoc cref="ICloneable{T}.Clone()"/>
        /// <remarks>
        /// Includes velocities.
        /// </remarks>
        public Transform Clone()
        {
            return new Transform
            {
                _base = _base,
                _velocity = _velocity
            };
        }

        /// <inheritdoc cref="IReplaceable{T}.ReplaceBy(T)"/>
        /// <remarks>
        /// Includes velocities.
        /// </remarks>
        public void ReplaceBy(Transform t) => ReplaceBy(t, true);

        internal void ReplaceBy(Transform t, bool isVelocity)
        {
            _base = t._base;

            if (isVelocity)
            {
                _velocity = t._velocity;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WorldLocation, WorldRotation, WorldScale);
        }

        public override string ToString()
        {
            return $"WorldLocation: {WorldLocation}\nWorldRotation: {WorldRotation}\nWorldScale   : {WorldScale}";
        }

        internal string ToString(bool isVelocity)
        {
            return ToString() + (isVelocity ?
                $"\nLocationVelocity: {WorldLocationVelocity}\nRotationVelocity: {WorldRotationVelocity}\nScaleVelocity   : {WorldScaleVelocity}" :
                ""
            );
        }
    }
}