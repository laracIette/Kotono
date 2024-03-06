using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal interface ISelectable : IObject
    {
        /// <summary>
        /// Determines wether the mouse is hovering the selectable.
        /// </summary>
        public bool IsHovered { get; }

        /// <summary>
        /// Determines wether the selectable is selected.
        /// </summary>
        public bool IsSelected { get; }

        /// <summary>
        /// Determines wether the selectable is active.
        /// </summary>
        public bool IsActive { get; }

        /// <summary>
        /// List of all the currently selected selectables.
        /// </summary>
        internal static List<ISelectable> Selected { get; } = [];

        /// <summary>
        /// The last selected selectable.
        /// </summary>
        internal static ISelectable? Active => Selected.LastOrDefault();
    }
}
