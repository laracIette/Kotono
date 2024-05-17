using Kotono.Graphics;

namespace Kotono.Utils.Coordinates
{
    internal readonly record struct NDCRect(Point Position, Point Size)
    {
        internal Point Position { get; } = Position.NDC;

        internal Point Size { get; } = Size / WindowComponentManager.ActiveViewport.Size;
    }
}
