using Kotono.Engine.UserInterface.Elements;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Collections.Generic;

namespace Kotono.Engine.UserInterface.Components
{
    internal abstract class WindowComponent : Object2D
    {
        private readonly Background _background = new();

        internal IEnumerable<WindowComponent> Connections 
            => ObjectManager.GetObjectsOfType<WindowComponent>(Rect.Overlaps);

        public override Point RelativeSize
        {
            get => base.RelativeSize;
            set
            {
                base.RelativeSize = value;
                _background.RelativeSize = value;
            }
        }

        internal Color BackgroundColor
        {
            get => _background.Color;
            set => _background.Color = value;
        }
    }
}
