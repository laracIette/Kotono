using System;

namespace Kotono.Utils.Exceptions
{
    internal class KotonoException : Exception
    {
        internal KotonoException(string message)
            : base($"error: {message}.") { }
    }
}
