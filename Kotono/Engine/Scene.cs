using Kotono.Engine.UserInterface;
using Kotono.Graphics;

namespace Kotono.Engine
{
    internal sealed class Scene : Object
    {
        private readonly ObjectExplorer _objectExplorer = new();

        private readonly Viewport _viewport = new();

        //internal static Scene Active { get; set; } = new();
    }
}
