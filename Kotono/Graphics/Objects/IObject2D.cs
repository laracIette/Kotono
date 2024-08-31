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
        /// The <see cref="IObject2D"/> the <see cref="IObject2D"/> is relative to.
        /// </summary>
        public IObject2D? Parent { get; set; }
    }
}
