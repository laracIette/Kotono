using Kotono.Utils.Coordinates;
using System.Collections.Generic;

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

        /// <summary>
        /// The objects that are dependant to the <see cref="IObject3D"/>.
        /// </summary>
        public IEnumerable<IObject3D> Children { get; }

        /// <summary>
        /// Get the children of type TChildren of the <see cref="IObject3D"/>.
        /// </summary>
        public IEnumerable<TChildren> GetChildren<TChildren>() where TChildren : IObject3D;

        /// <summary>
        /// Get the first child of type TChild of the <see cref="IObject3D"/>.
        /// </summary>
        public TChild? GetChild<TChild>() where TChild : IObject3D;
    }
}
