using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

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



#pragma warning disable IDE1006 // Naming Styles

#if DEBUG
        private string? __type => GetType().FullName;
#endif

        protected static string str(params object?[] objects)
        {
            var builder = new StringBuilder();
            foreach (var obj in objects)
            {
                builder.Append(obj + " ");
            }
            return builder.ToString();
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
