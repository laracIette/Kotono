using System;

namespace Kotono.Utils
{
    [Flags]
    internal enum Visibility
    {
        None = 0x0,

        Editor = 0x1,
        Playing = 0x2,

        EditorAndPlaying = Editor | Playing
    }
}
