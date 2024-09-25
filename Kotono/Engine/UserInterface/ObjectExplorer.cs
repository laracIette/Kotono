using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.UserInterface
{
    internal sealed class ObjectExplorer : Object2D
    {
        private CustomList<Drawable> Drawables { get; } = [];

        internal ObjectExplorer()
        {
            Drawables.AddAction = d => { };
            Drawables.RemoveAction = d => { };
        }
    }
}
