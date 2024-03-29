﻿using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background(Rect dest, Color color, Viewport viewport)
        : RoundedBox(
            new RoundedBoxSettings
            {
                Dest = dest,
                Color = color,
                FallOff = 1.0f,
                CornerSize = 15.0f
            }
        ),
        IElement
    {
        public Viewport Viewport => viewport;
    }
}
