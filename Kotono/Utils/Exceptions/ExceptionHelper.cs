﻿namespace Kotono.Utils.Exceptions
{
    internal static class ExceptionHelper
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
