﻿using OpenTK.Mathematics;
using System;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [Serializable]
    internal class Transform : Object, IEquatable<Transform>, ICloneable<Transform>
    {
        /// <summary> 
        /// The location of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeLocation { get; set; } = DefaultLocation;

        /// <summary> 
        /// The rotation of the Transform relative to its parent. 
        /// </summary>
        public Rotator RelativeRotation { get; set; } = DefaultRotation;

        /// <summary> 
        /// The scale of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeScale { get; set; } = DefaultScale;

        /// <summary> 
        /// The location of the Transform. 
        /// </summary>
        internal Vector WorldLocation
        {
            get => RelativeLocation + ParentWorldLocation;
            set => RelativeLocation = value - ParentWorldLocation;
        }

        /// <summary> 
        /// The rotation of the Transform. 
        /// </summary>
        internal Rotator WorldRotation
        {
            get => RelativeRotation + ParentWorldRotation;
            set => RelativeRotation = value - ParentWorldRotation;
        }

        /// <summary> 
        /// The scale of the Transform. 
        /// </summary>
        internal Vector WorldScale
        {
            get => RelativeScale * ParentWorldScale;
            set => RelativeScale = value / ParentWorldScale;
        }

        /// <summary>
        /// The velocity of the Transform's relative location.
        /// </summary>
        internal Vector LocationVelocity { get; set; } = DefaultLocation;

        /// <summary>
        /// The velocity of the Transform's relative rotation.
        /// </summary>
        internal Rotator RotationVelocity { get; set; } = DefaultRotation;

        /// <summary>
        /// The velocity of the Transform's relative scale.
        /// </summary>
        internal Vector ScaleVelocity { get; set; } = DefaultScale;

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
        /// The transform the Transform is relative to.
        /// </summary>
        internal Transform? Parent { get; set; } = null;

        internal Vector ParentWorldLocation => Parent?.WorldLocation ?? DefaultLocation;

        internal Rotator ParentWorldRotation => Parent?.WorldRotation ?? DefaultRotation;

        internal Vector ParentWorldScale => Parent?.WorldScale ?? DefaultScale;

        /// <summary>
        /// The model matrix of the Transform.
        /// </summary>
        internal Matrix4 Model =>
            Vector.CreateScaleMatrix(WorldScale)
            * Rotator.CreateRotationMatrix(WorldRotation)
            * Vector.CreateTranslationMatrix(WorldLocation);

        /// <summary> 
        /// A Transform with default location, rotation and scale. 
        /// </summary>
        internal static Transform Default => new Transform(DefaultLocation, DefaultRotation, DefaultScale);

        internal static Vector DefaultLocation => Vector.Zero;

        internal static Rotator DefaultRotation => Rotator.Zero;

        internal static Vector DefaultScale => Vector.Unit;

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
            RelativeLocation += LocationVelocity * Time.Delta;
            RelativeRotation += RotationVelocity * Time.Delta;
            RelativeScale += ScaleVelocity * Time.Delta;
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

        public bool Equals(Transform? other)
        {
            if (other is null)
            {
                return false;
            }

            return RelativeLocation == other.RelativeLocation
                && RelativeRotation == other.RelativeRotation
                && RelativeScale == other.RelativeScale
                && LocationVelocity == other.LocationVelocity
                && RotationVelocity == other.RotationVelocity
                && ScaleVelocity == other.ScaleVelocity;
        }

        /// <summary>
        /// Get a copy of this <see cref="Transform"/>, with copied velocities.
        /// </summary>
        public Transform Clone()
        {
            return new Transform
            {
                RelativeLocation = RelativeLocation,
                RelativeRotation = RelativeRotation,
                RelativeScale = RelativeScale,
                LocationVelocity = LocationVelocity,
                RotationVelocity = RotationVelocity,
                ScaleVelocity = ScaleVelocity
            };
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
                $"\nLocationVelocity: {LocationVelocity}\nRotationVelocity: {RotationVelocity}\nScaleVelocity   : {ScaleVelocity}" :
                ""
            );
        }
    }
}
