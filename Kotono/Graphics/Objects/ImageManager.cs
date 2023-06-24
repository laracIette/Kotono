using System.Collections.Generic;
using System;

namespace Kotono.Graphics.Objects
{
    internal class ImageManager
    {
        private readonly List<Image> _images = new();

        internal ImageManager() { }

        internal void Create(Image image)
        {
            _images.Add(image);
        }

        internal void Delete(Image image)
        {
            if (_images.Count <= 0)
            {
                KT.Print($"The number of Image is already at 0.");
            }
            else
            {
                _images.Remove(image);
            }
        }

        internal void Update()
        {
            foreach (var image in _images)
            {
                image.Update();
            }
        }

        internal void UpdateShaders()
        {
            foreach (var image in _images)
            {
                image.UpdateShaders();
            }
        }

        internal void Draw()
        {
            foreach (var image in _images)
            {
                if (image.IsDraw)
                {
                    image.Draw(); 
                }
            }
        }
    }
}
