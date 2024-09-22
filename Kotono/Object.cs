using Kotono.Graphics.Objects;
using System;

namespace Kotono
{
    internal abstract class Object : IObject
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public virtual bool IsUpdate { get; set; } = true;

        public bool IsDelete { get; private set; } = false;

        public float CreationTime { get; } = Time.Now;

        public float TimeSinceCreation => Time.Now - CreationTime;

        internal Object() => ObjectManager.Create(this);

        public virtual void Update() { }

        public virtual void Dispose() => IsDelete = true;
    }
}
