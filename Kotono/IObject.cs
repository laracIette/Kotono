using System;

namespace Kotono
{
    internal interface IObject : IDisposable
    {
        public bool IsUpdate { get; set; }

        public bool IsDelete { get; }

        //public bool HasInput { get; set; }

        public void Update();
    }
}
