using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class TriangleManager
    {
        private readonly List<Triangle> _triangles = new();

        internal TriangleManager() { }

        internal void Create(Triangle triangle)
        {
            _triangles.Add(triangle);
        }

        internal void Delete(Triangle triangle)
        {
            if (_triangles.Count <= 0)
            {
                KT.Print($"The number of Triangle is already at 0.");
            }
            else
            {
                _triangles.Remove(triangle);
            }
        }

        internal void Init()
        {
            foreach (var triangle in _triangles)
            {
                triangle.Init();
            }
        }

        internal void Update()
        {
            foreach (var triangle in _triangles)
            {
                triangle.Update();
            }
        }

        internal void Draw()
        {
            foreach (var triangle in _triangles)
            {
                if (triangle.IsDraw)
                {
                    triangle.Draw();
                }
            }
        }
    }
}
