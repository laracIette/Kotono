namespace Kotono.Graphics.Objects
{
    internal interface ISelectable : IObject
    {
        /// <summary>
        /// Wether the mouse is currently hovering the <see cref="ISelectable"/>.
        /// </summary>
        public bool IsHovered { get; }

        /// <summary>
        /// Wether the <see cref="ISelectable"/> is currently selected.
        /// </summary>
        public bool IsSelected { get; }

        /// <summary>
        /// Wether the <see cref="ISelectable"/> is currently active.
        /// </summary>
        public bool IsActive { get; }
    }
}
