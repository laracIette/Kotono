using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class BoxRoundedCornersManager
    {
        private readonly List<BoxRoundedCorners> _boxes = new();

        internal BoxRoundedCornersManager() { }

        internal void Create(BoxRoundedCorners box)
        {
            _boxes.Add(box);
        }

        internal void Delete(BoxRoundedCorners box)
        {
            _boxes.Remove(box);
        }

        internal void Init()
        {
            foreach (var box in _boxes)
            {
                box.Init();
            }
        }

        internal void Update()
        {
            foreach (var box in _boxes)
            {
                box.Update();
            }
        }

        internal void UpdateShaders()
        {
            foreach (var box in _boxes)
            {
                box.UpdateShaders();
            }
        }

        internal void Draw()
        {
            foreach (var box in _boxes)
            {
                if (box.IsDraw)
                {
                    box.Draw(); 
                }
            }
        }
    }
}
