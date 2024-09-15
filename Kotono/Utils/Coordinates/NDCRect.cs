using Kotono.Graphics;
using OpenTK.Mathematics;

namespace Kotono.Utils.Coordinates
{
    public readonly record struct NDCRect(Point Position, Point Size)
    {
        public readonly Point Position = Position.NDC;

        public readonly Point Size = Size / Viewport.Active.WorldSize;

        public readonly Matrix4 Model =>
            Matrix4.CreateScale(Size.X, Size.Y, 1.0f)
            * Rotator.Zero.RotationMatrix
            * Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);
    }
}
