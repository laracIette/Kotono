using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject3D : IDrawable, ITransform
    {
        public Transform Transform { get; set; }

        public new IObject3D? Parent { get; set; }
    }
}
