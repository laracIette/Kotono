using System;

namespace Kotono.Utils.Exceptions
{
    internal sealed class SwitchException(Type switchType, object obj)
        : KotonoException($"switch of type {switchType} doesn't handle {obj}");
}
