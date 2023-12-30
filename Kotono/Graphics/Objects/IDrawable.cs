using System;

namespace Kotono.Graphics.Objects
{
    public interface IDrawable
    {
        public bool IsDraw { get; set; }

        public void Update();

        public void Draw();
    }
}
