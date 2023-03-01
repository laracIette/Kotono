using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    interface ITexture2D : IDrawable
    {
        Vector2 Position { get; set; }

        Vector2 Size { get; set; }

        float Angle { get; set; }
    }
}
