using System.Collections.Generic;

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
            _images.Remove(image);
        }

        internal void Init()
        {
            foreach (var image in _images)
            {
                image.Init();
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
