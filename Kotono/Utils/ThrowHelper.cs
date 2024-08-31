using Kotono.Utils.Exceptions;

namespace Kotono.Utils
{
    internal static class ThrowHelper
    {
        internal static void ThrowIf(bool condition, string message)
        {
            if (condition)
            {
                throw new KotonoException(message);
            }
        }
    }
}
