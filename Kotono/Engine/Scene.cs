using Kotono.Engine.UserInterface;
using Kotono.Graphics;
using System;

namespace Kotono.Engine
{
    internal sealed class Scene : Object
    {
        internal static Scene? Active { get; set; } = null;

        private readonly ObjectExplorer _objectExplorer = new();

        private readonly Viewport _viewport = new();

        private readonly string[] _guids = [];
    }
}
