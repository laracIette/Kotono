using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox : IDrawable, IObject3D
    {
        public bool Collides(IHitbox h);

        public bool IsColliding { get; }

        public Color Color { get; set; }

        public List<IHitbox> Collisions { get; set; }
    }
}
