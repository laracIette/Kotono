using Kotono.Graphics.Objects.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal interface ISelectable : IObject
    {
        /// <summary>
        /// Determines wether the selectable is selected.
        /// </summary>
        public bool IsSelected { get; }

        /// <summary>
        /// List of all the currently selected selectables.
        /// </summary>
        internal static List<ISelectable> Selected { get; } = [];

        /// <summary>
        /// Determines wether the selectable is the last selected selectable.
        /// </summary>
        internal static ISelectable? Active => Selected.LastOrDefault();
    }
}
