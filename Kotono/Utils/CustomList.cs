using System;
using System.Collections.Generic;

namespace Kotono.Utils
{
    internal sealed class CustomList<T> : List<T>
    {
        internal Action<T>? AddAction { get; set; } = null;

        internal Action<T>? RemoveAction { get; set; } = null;

        internal new void Add(T item)
        {
            AddAction?.Invoke(item);
            base.Add(item);
        }

        internal new void Remove(T item)
        {
            RemoveAction?.Invoke(item);
            base.Remove(item);
        }
    }
}
