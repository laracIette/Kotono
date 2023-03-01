using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    interface IDrawable
    {
        bool IsDraw { get; set; }

        void Draw();
    }
}
