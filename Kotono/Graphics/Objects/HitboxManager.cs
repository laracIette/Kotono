using Kotono.Graphics.Objects.Hitboxes;
using System;

namespace Kotono.Graphics.Objects
{
    public sealed class HitboxManager
    {
        private static readonly Lazy<Box> lazyBox = new(() => new Box());

        public static Box Box => lazyBox.Value;

        private HitboxManager() { }
    }
}
