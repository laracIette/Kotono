using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal interface ISelectable : IObject
    {
        /// <summary>
        /// Wether the mouse is hovering the selectable.
        /// </summary>
        public bool IsHovered { get; }

        /// <summary>
        /// Wether the selectable is selected.
        /// </summary>
        public bool IsSelected { get; }

        /// <summary>
        /// Wether the selectable is active.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// List of all the currently selected selectables.
        /// </summary>
        internal static List<ISelectable> Selected { get; } = [];

        /// <summary>
        /// List of all the currently selected 2D objects.
        /// </summary>
        internal static List<IObject2D> Selected2D => Selected.OfType<IObject2D>().ToList();

        /// <summary>
        /// List of all the currently selected 3D objects.
        /// </summary>
        internal static List<IObject3D> Selected3D => Selected.OfType<IObject3D>().ToList();

        /// <summary>
        /// The last selected selectable.
        /// </summary>
        internal static ISelectable? Active => Selected.LastOrDefault();

        /// <summary>
        /// The last selected selectable as a 2D object.
        /// </summary>
        internal static IObject2D? Active2D => Active as IObject2D;

        /// <summary>
        /// The last selected selectable as a 3D object.
        /// </summary>
        internal static IObject3D? Active3D => Active as IObject3D;
    }
}
