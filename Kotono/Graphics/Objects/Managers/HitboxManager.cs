using Kotono.Graphics.Objects.Hitboxes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public class HitboxManager : DrawableManager
    {
        private readonly List<IHitbox> _hitboxes = new();

        public HitboxManager()
            : base() { }

        public void Create(IHitbox hitbox)
        {
            _hitboxes.Add(hitbox);
            base.Create(hitbox);
        }

        public void Delete(IHitbox hitbox)
        {
            _hitboxes.Remove(hitbox);
            base.Delete(hitbox);
        }

        public List<IHitbox> GetAll()
        {
            return _hitboxes;
        }
    }
}
