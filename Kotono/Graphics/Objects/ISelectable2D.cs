using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal interface ISelectable2D : ISelectable, IObject2D
    {
        /// <summary>
        /// List of all the currently selected <see cref="ISelectable2D"/>.
        /// </summary>
        internal static List<ISelectable2D> Selected { get; } = [];

        /// <summary>
        /// The latest selected <see cref="ISelectable2D"/>.
        /// </summary>
        internal static ISelectable2D? Active => Selected.LastOrDefault();
    }
}
