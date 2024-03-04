using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject3D : IDrawable
    {
        public Transform Transform { get; set; }

        public Vector Location { get; set; }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }

        public Vector LocationVelocity { get; set; }

        public Vector RotationVelocity { get; set; }

        public Vector ScaleVelocity { get; set; }
    }
}
