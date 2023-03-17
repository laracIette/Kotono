using Kotono.Graphics.Objects.Hitboxes;
using System;

namespace Kotono.Graphics.Objects
{
    public sealed class HitboxManager
    {
        private static readonly Lazy<Box> _box = new(() => new());

        private static readonly Lazy<Distance> _distance = new(() => new());

        public static Box Box => _box.Value;

        public static Distance Distance => _distance.Value;

        private HitboxManager() { }
    }
}
