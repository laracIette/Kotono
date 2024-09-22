using Kotono.Utils.Coordinates;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal interface IObject2D : IDrawable, IRect
    {
        /// <summary>
        /// The rect of the <see cref="IObject2D"/>.
        /// </summary>
        public Rect Rect { get; }

        /// <summary>
        /// The layer of the <see cref="IObject2D"/>, the greater the later it is drawn.
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// The <see cref="IObject2D"/> the <see cref="IObject2D"/> is relative to.
        /// </summary>
        public IObject2D? Parent { get; set; }

        /// <summary>
        /// The objects that are dependant to the <see cref="IObject2D"/>.
        /// </summary>
        public IEnumerable<IObject2D> Children { get; }

        /// <summary>
        /// Get the children of type TChildren of the <see cref="IObject2D"/>.
        /// </summary>
        public IEnumerable<TChildren> GetChildren<TChildren>() where TChildren : IObject2D;

        /// <summary>
        /// Get the first child of type TChild of the <see cref="IObject2D"/>.
        /// </summary>
        public TChild? GetChild<TChild>() where TChild : IObject2D;
    }
}
