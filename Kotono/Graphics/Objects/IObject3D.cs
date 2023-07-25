using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public interface IObject3D
    {
        public Transform Transform { get; set; }

        public Vector Location { get; set; }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }
    }
}
