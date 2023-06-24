using Kotono.Graphics.Objects.Hitboxes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal class HitboxManager
    {
        private readonly List<IHitbox> _hitboxes = new();

        internal HitboxManager() { }

        internal void Create(IHitbox hitbox)
        {
            _hitboxes.Add(hitbox);
        }

        internal void Delete(IHitbox hitbox)
        {
            if (_hitboxes.Count <= 0)
            {
                KT.Print($"The number of Hitbox is already at 0.");
            }
            else
            {
                _hitboxes.Remove(hitbox);
            }
        }

        internal List<IHitbox> GetAll()
        { 
            return _hitboxes; 
        }

        internal void Update()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Update();
            }
        }

        internal void Draw()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Draw();
            }
        }
    }
}
