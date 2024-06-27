namespace Kotono.Utils.Coordinates
{
    internal interface IRect
    {
        /// <summary> 
        /// The base size component of the Rect. 
        /// </summary>
        public Point BaseSize { get; set; }

        /// <summary>
        /// The size component of the Rect.
        /// </summary>
        public Point Size { get; set; }

        /// <summary>
        /// The position component of the Rect.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// The rotation component of the Rect.
        /// </summary>
        public Rotator Rotation { get; set; }

        /// <summary>
        /// The scale component of the Rect.
        /// </summary>
        public Point Scale { get; set; }
    }
}
