using OpenTK.Mathematics;
using System;
using System.Text.Json.Serialization;

namespace Kotono.Utils.Coordinates
{
    [Serializable]
    public class Transform : IEquatable<Transform>
    {
        /// <summary> 
        /// The location component of the Transform. 
        /// </summary>
        public Vector Location { get; set; }

        /// <summary> 
        /// The rotation component of the Transform. 
        /// </summary>
        public Vector Rotation { get; set; }

        /// <summary> 
        /// The scale component of the Transform. 
        /// </summary>
        public Vector Scale { get; set; }

        /// <summary>
        /// The velocity of the Transform's location.
        /// </summary>
        internal Vector LocationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's rotation.
        /// </summary>
        internal Vector RotationVelocity { get; set; }

        /// <summary>
        /// The velocity of the Transform's scale.
        /// </summary>
        internal Vector ScaleVelocity { get; set; }

        /// <summary> 
        /// The right vector of the Transform. 
        /// </summary>
        internal Vector Right => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitX);

        /// <summary> 
        /// The up vector of the Transform. 
        /// </summary>
        internal Vector Up => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitY);

        /// <summary> 
        /// The forward vector of the Transform. 
        /// </summary>
        internal Vector Forward => (Vector)(Quaternion.FromEulerAngles((Vector3)Rotation) * Vector3.UnitZ);

        /// <summary>
        /// The model matrix of the Transform.
        /// </summary>
        internal Matrix4 Model =>
            Matrix4.CreateScale((Vector3)Scale)
            * Matrix4.CreateRotationX(Rotation.X)
            * Matrix4.CreateRotationY(Rotation.Y)
            * Matrix4.CreateRotationZ(Rotation.Z)
            * Matrix4.CreateTranslation((Vector3)Location);

        /// <summary> 
        /// A Transform with Location = Vector.Zero, Rotation = Vector.Zero, Scale = Vector.Unit. 
        /// </summary>
        internal static Transform Default => new Transform(Vector.Zero, Vector.Zero, Vector.Unit);

        [JsonConstructor]
        internal Transform()
        {
            Location = Vector.Zero;
            Rotation = Vector.Zero;
            Scale = Vector.Zero;
            LocationVelocity = Vector.Zero;
            RotationVelocity = Vector.Zero;
            ScaleVelocity = Vector.Zero;
        }

        internal Transform(Transform t)
        {
            Location = t.Location;
            Rotation = t.Rotation;
            Scale = t.Scale;
            LocationVelocity = t.LocationVelocity;
            RotationVelocity = t.RotationVelocity;
            ScaleVelocity = t.ScaleVelocity;
        }

        internal Transform(Vector location, Vector rotation, Vector scale)
        {
            Location = location;
            Rotation = rotation;
            Scale = scale;
            LocationVelocity = Vector.Zero;
            RotationVelocity = Vector.Zero;
            ScaleVelocity = Vector.Zero;
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

            return Location == other.Location
                && Rotation == other.Rotation
                && Scale == other.Scale
                && LocationVelocity == other.LocationVelocity
                && RotationVelocity == other.RotationVelocity
                && ScaleVelocity == other.ScaleVelocity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Rotation, Scale);
        }

        public override string ToString()
        {
            return $"Location: {Location}\nRotation: {Rotation}\nScale   : {Scale}";
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
