using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox
    {
        public void Update();

        public void Draw();

        public bool Collides(Box b);

        public Vector3 Position { get; set; }

        public Vector3 Angle { get; set; }

        public Vector3 Scale { get; set; }

        public Vector3 Color { get; set; }
    }
}
