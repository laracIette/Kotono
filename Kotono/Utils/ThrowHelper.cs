using Kotono.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotono.Utils
{
    internal class ThrowHelper
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
