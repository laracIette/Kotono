using System.Runtime.CompilerServices;

namespace Kotono.Utils.Exceptions
{
    internal static class ExceptionHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ThrowIf(bool condition, string message)
        {
            if (condition)
            {
                throw new KotonoException(message);
            }
        }
    }
}
