using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal interface IObject3D : IDrawable, ITransform
    {
        /// <summary>
        /// The transform of the <see cref="IObject3D"/>.
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// The <see cref="IObject3D"/> the <see cref="IObject3D"/> is relative to.
        /// </summary>
        public IObject3D? Parent { get; set; }
    }
}
