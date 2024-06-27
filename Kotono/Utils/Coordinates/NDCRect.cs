using Kotono.Graphics;
using OpenTK.Mathematics;

namespace Kotono.Utils.Coordinates
{
    internal readonly record struct NDCRect(Point Position, Point Size)
    {
        internal Point Position { get; } = Position.NDC;

        internal Point Size { get; } = Size / Viewport.Active.Size;

        public Matrix4 Model =>
            Matrix4.CreateScale(Size.X, Size.Y, 1.0f)
            * Rotator.Zero.RotationMatrix
            * Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);
    }
}
