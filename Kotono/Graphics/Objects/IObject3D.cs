using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject3D : IDrawable
    {
        public Transform Transform { get; set; }

        public Vector RelativeLocation { get; set; }

        public Vector RelativeRotation { get; set; }

        public Vector RelativeScale { get; set; }

        public Vector LocationVelocity { get; set; }

        public Vector RotationVelocity { get; set; }

        public Vector ScaleVelocity { get; set; }
    }
}
