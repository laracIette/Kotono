using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects.Shapes
{
    internal class Line(Vector start, Vector end) 
        : Shape([start, end], false)
    {
    }
}
