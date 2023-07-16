using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public interface IHitbox
    {
        public void Init();

        public void Update();

        public void Draw();

        public bool Collides(IHitbox h);

        public bool IsColliding { get; }

        public Transform Transform { get; }

        public Vector Location { get; set; }

        public Vector Rotation { get; set; }

        public Vector Scale { get; set; }

        public Color Color { get; set; }

        public List<IHitbox> Collisions { get; set; }
    }
}
