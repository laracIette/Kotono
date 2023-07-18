using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class RoundedBoxManager
    {
        private readonly List<RoundedBox> _boxes = new();

        internal RoundedBoxManager() { }

        internal void Create(RoundedBox box)
        {
            _boxes.Add(box);
        }

        internal void Delete(RoundedBox box)
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
