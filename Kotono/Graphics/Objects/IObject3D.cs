using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject3D : IDrawable
    {
        public Transform Transform { get; set; }

        public Vector RelativeLocation { get; set; }

        public Rotator RelativeRotation { get; set; }

        public Vector RelativeScale { get; set; }

        public Vector WorldLocation { get; set; }

        public Rotator WorldRotation { get; set; }

        public Vector WorldScale { get; set; }

        public Vector LocationVelocity { get; set; }

        public Rotator RotationVelocity { get; set; }

        public Vector ScaleVelocity { get; set; }

        public IObject3D? Parent { get; set; }
    }
}
