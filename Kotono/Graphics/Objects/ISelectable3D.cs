using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal interface ISelectable3D : ISelectable, IObject3D
    {
        /// <summary>
        /// List of all the currently selected <see cref="ISelectable3D"/>.
        /// </summary>
        internal static List<ISelectable3D> Selected { get; } = [];

        /// <summary>
        /// The latest selected <see cref="ISelectable3D"/>.
        /// </summary>
        internal static ISelectable3D? Active => Selected.LastOrDefault();
    }
}
