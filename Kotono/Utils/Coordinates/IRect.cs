namespace Kotono.Utils.Coordinates
{
    internal interface IRect
    {
        /// <summary>
        /// The point to which the <see cref="IRect"/> is anchored.
        /// </summary>
        public Anchor Anchor { get; set; }

        /// <summary> 
        /// The base size component of the <see cref="IRect"/>. 
        /// </summary>
        public Point BaseSize { get; set; }

        /// <summary>
        /// The size component of the <see cref="IRect"/>.
        /// </summary>
        public Point RelativeSize { get; set; }

        /// <summary>
        /// The position component of the <see cref="IRect"/>.
        /// </summary>
        public Point RelativePosition { get; set; }

        /// <summary>
        /// The rotation component of the <see cref="IRect"/>.
        /// </summary>
        public Rotator RelativeRotation { get; set; }

        /// <summary>
        /// The scale component of the <see cref="IRect"/>.
        /// </summary>
        public Point RelativeScale { get; set; }

        /// <summary>
        /// The size component of the <see cref="IRect"/>.
        /// </summary>
        public Point WorldSize { get; set; }

        /// <summary>
        /// The position component of the <see cref="IRect"/>.
        /// </summary>
        public Point WorldPosition { get; set; }

        /// <summary>
        /// The rotation component of the <see cref="IRect"/>.
        /// </summary>
        public Rotator WorldRotation { get; set; }

        /// <summary>
        /// The scale component of the <see cref="IRect"/>.
        /// </summary>
        public Point WorldScale { get; set; }
    }
}
