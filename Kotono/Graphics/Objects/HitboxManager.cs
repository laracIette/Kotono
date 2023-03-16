using Kotono.Graphics.Objects.Hitboxes;
using System;

namespace Kotono.Graphics.Objects
{
    public sealed class HitboxManager
    {
        private static readonly Lazy<Box> _box = new(() => new Box());

        public static Box Box => _box.Value;

        private HitboxManager() { }
    }
}
