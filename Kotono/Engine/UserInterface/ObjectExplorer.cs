using System.Collections.Generic;

namespace Kotono.Engine.UserInterface
{
    internal class ObjectExplorer : Object2D
    {
        private readonly List<Object> _objects;

        internal ObjectExplorer()
            : base()
        {
            _objects = [];
        }
    }
}
