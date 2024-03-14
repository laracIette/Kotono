using OpenTK.Mathematics;
using System;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [Serializable]
    public class Transform : IEquatable<Transform>
    {
        /// <summary> 
        /// The location of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeLocation { get; set; }

        /// <summary> 
        /// The rotation of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeRotation { get; set; }

        /// <summary> 
        /// The scale of the Transform relative to its parent. 
        /// </summary>
        public Vector RelativeScale { get; set; }

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
        internal Vector WorldRotation
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
        internal Vector LocationVelocity { get; set; } = Vector.Zero;

        /// <summary>
        /// The velocity of the Transform's relative rotation.
        /// </summary>
        internal Vector RotationVelocity { get; set; } = Vector.Zero;

        /// <summary>
        /// The velocity of the Transform's relative scale.
        /// </summary>
        internal Vector ScaleVelocity { get; set; } = Vector.Unit;

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
            Matrix4.CreateScale((Vector3)WorldScale)
            * Matrix4.CreateRotationX(WorldRotation.X)
            * Matrix4.CreateRotationY(WorldRotation.Y)
            * Matrix4.CreateRotationZ(WorldRotation.Z)
            * Matrix4.CreateTranslation((Vector3)WorldLocation);

        /// <summary>
        /// The transform the Transform is relative to.
        /// </summary>
        internal Transform? Parent { get; set; } = null;

        internal Vector ParentWorldLocation => Parent?.WorldLocation ?? Vector.Zero;

        internal Vector ParentWorldRotation => Parent?.WorldRotation ?? Vector.Zero;

        internal Vector ParentWorldScale => Parent?.WorldScale ?? Vector.Unit;

        /// <summary> 
        /// A Transform with Location = Vector.Zero, Rotation = Vector.Zero, Scale = Vector.Unit. 
        /// </summary>
        internal static Transform Default => new Transform(Vector.Zero, Vector.Zero, Vector.Unit);

        [JsonConstructor]
        internal Transform()
        {
            RelativeLocation = Vector.Zero;
            RelativeRotation = Vector.Zero;
            RelativeScale = Vector.Unit;
        }

        internal Transform(Transform t)
        {
            RelativeLocation = t.RelativeLocation;
            RelativeRotation = t.RelativeRotation;
            RelativeScale = t.RelativeScale;
            LocationVelocity = t.LocationVelocity;
            RotationVelocity = t.RotationVelocity;
            ScaleVelocity = t.ScaleVelocity;
        }

        internal Transform(Vector location, Vector rotation, Vector scale)
        {
            RelativeLocation = location;
            RelativeRotation = rotation;
            RelativeScale = scale;
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

        public override int GetHashCode()
        {
            return HashCode.Combine(RelativeLocation, RelativeRotation, RelativeScale);
        }

        public override string ToString()
        {
            return $"RelativeLocation: {RelativeLocation}\nRelativeRotation: {RelativeRotation}\nRelativeScale   : {RelativeScale}";
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
