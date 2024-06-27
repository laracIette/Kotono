using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal interface IDrawable : IObject
    {
        public bool IsDraw { get; set; }

        public Color Color { get; set; }

        public Viewport Viewport { get; set; }

        public IDrawable? Parent { get; set; }

        public List<Drawable> Childrens { get; }

        public void Draw();
    }
}
