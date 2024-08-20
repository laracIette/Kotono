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
        /// The object to which the <see cref="IObject3D"/> is dependant.
        /// </summary>
        public IObject3D? Parent { get; set; }
    }
}
