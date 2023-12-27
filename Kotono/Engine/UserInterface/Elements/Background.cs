﻿using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.UserInterface.Elements
{
    internal class Background(Rect dest, Viewport viewport)
        : RoundedBox(dest, Color.FromHex("#FFF1"), 0, 1, 15), 
        IElement
    {
        public Viewport Viewport => viewport;
    }
}
