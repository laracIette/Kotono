using Kotono.Engine.UserInterface;
using Kotono.Graphics;
using Kotono.Utils.Coordinates;

namespace Kotono.Engine
{
    internal class Scene
    {
        internal static Scene Active { get; set; } = new();

        internal string Name { get; set; } = string.Empty;

        private readonly ObjectExplorer _objectExplorer = new();

        private readonly Viewport _viewport = new();

        internal Scene()
        {

        }
    }
}
