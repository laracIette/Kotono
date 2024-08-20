using Kotono.Utils.Coordinates;

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
        /// The object to which the <see cref="IObject2D"/> is dependant.
        /// </summary>
        public IObject2D? Parent { get; set; }
    }
}
