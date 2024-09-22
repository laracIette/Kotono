using System;

namespace Kotono.Utils.Exceptions
{
    internal class KotonoException(string message)
        : Exception($"error: {message}.");
}
