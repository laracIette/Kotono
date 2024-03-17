using System;

namespace Kotono.Utils.Exceptions
{
    internal class SwitchException : KotonoException
    {
        internal SwitchException(Type switchType, object obj)
            : base($"switch of type {switchType} doesn't handle {obj}")
        { }
    }
}
