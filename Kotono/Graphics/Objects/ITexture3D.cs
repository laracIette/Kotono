using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    interface ITexture3D : IDrawable
    {
        Vector3 Position { get; set; }

        Vector3 Angle { get; set; }
    }
}
