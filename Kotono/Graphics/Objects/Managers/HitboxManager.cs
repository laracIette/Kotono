using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    internal class HitboxManager : DrawableManager<IHitbox>
    {
        internal HitboxManager()
            : base() { }

        internal List<IHitbox> GetAll()
        {
            return _drawables;
        }
    }
}
