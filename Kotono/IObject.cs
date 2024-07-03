using System;

namespace Kotono
{
    internal interface IObject : IDisposable
    {
        public bool IsUpdate { get; set; }

        public bool IsDelete { get; }

        public float CreationTime { get; }

        public float TimeSinceCreation { get; }

        public void Update();
    }
}
