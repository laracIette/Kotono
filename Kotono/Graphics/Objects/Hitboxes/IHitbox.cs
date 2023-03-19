using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox
    {
        public bool Collides(Box b);

        public Vector3 Position { get; set; }
    }
}
