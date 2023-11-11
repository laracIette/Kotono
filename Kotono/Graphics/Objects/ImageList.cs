using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public class ImageList : IObject2D
    {
        protected readonly List<Image> _images;

        public int Count => _images.Count;

        public Rect Dest
        {
            get => (Count > 0) ? _images[0].Dest : throw new Exception("error: cannot access _images[0].Dest, _images is empty.");
            set
            {
                foreach (var image in _images)
                {
                    image.Dest = value;
                }
            }
        }

        public int Layer
        {
            get => (Count > 0) ? _images[0].Layer : throw new Exception("error: cannot access _images[0].Layer, _images is empty.");
            set
            {
                foreach (var image in _images)
                {
                    image.Layer = value;
                }
            }
        }

        public bool IsDraw { get; private set; }

        public ImageList(List<Image> images)
        {
            _images = images;
            Hide();
        }

        public void Draw()
        {
            foreach (var image in _images)
            {

            }
        }

        public void Init()
        {
            foreach (var image in _images)
            {

            }
        }

        public void Save()
        {
            foreach (var image in _images)
            {

            }
        }

        public void Show()
        {
            IsDraw = true;
            foreach (var image in _images)
            {
                image.Show();
            }
        }

        public void Hide()
        {
            IsDraw = false;
            foreach (var image in _images)
            {
                image.Hide();
            }
        }

        public virtual void Update()
        {
            foreach (var image in _images)
            {

            }
        }

        public void UpdateShaders()
        {
            foreach (var image in _images)
            {

            }
        }

        public void Clear()
        {
            foreach (var image in _images)
            {
                ObjectManager.DeleteImage(image);
            }
            _images.Clear();
        }

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
