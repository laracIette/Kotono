using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox
    {
        public void Update();

        public void Draw();

        public bool Collides(IHitbox b);

        public bool IsColliding();

        public Vector Position { get; set; }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }

        public Vector Color { get; set; }

        public List<IHitbox> Collisions { get; set; }
    }
}
