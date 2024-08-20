using System;

namespace Kotono.Utils
{
    [Flags]
    internal enum Visibility
    {
        None = 0b0000,

        Editor = 0b0001,
        Playing = 0b0010,

        EditorAndPlaying = Editor | Playing
    }
}
