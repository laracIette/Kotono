using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public class HitboxManager : DrawableManager<IHitbox>
    {
        public HitboxManager()
            : base() { }

        public List<IHitbox> GetAll()
        {
            return _drawables;
        }
    }
}
