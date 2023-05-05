using System.Collections.Generic;
using System;

namespace Kotono.Graphics.Objects
{
    internal class ImageManager
    {
        private readonly List<Image> _images = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _imageIndex = 0;

        internal ImageManager() { }

        internal int Create(Image image)
        {
            _indexOffset[_imageIndex] = _images.Count;

            _images.Add(image);

            return _imageIndex++;
        }

        internal void Delete(int index)
        {
            if (_images.Count <= 0)
            {
                throw new Exception($"The number of Image is already at 0.");
            }

            _images.RemoveAt(_indexOffset[index]);
            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
        }

        internal Rect GetRect(int index)
            => _images[_indexOffset[index]].Dest;

        internal void SetX(int index, float x)
            => _images[_indexOffset[index]].Dest.X = x;
        
        internal void SetY(int index, float y)
            => _images[_indexOffset[index]].Dest.Y = y;
        
        internal void SetW(int index, float w)
            => _images[_indexOffset[index]].Dest.W = w;
        
        internal void SetH(int index, float h)
            => _images[_indexOffset[index]].Dest.H = h;

        internal void Transform(int index, Rect transformation, double time)
            => _images[_indexOffset[index]].Transform(transformation, time);
        
        internal void TransformTo(int index, Rect dest, double time)
            => _images[_indexOffset[index]].TransformTo(dest, time);

        internal void Show(int index)
            => _images[_indexOffset[index]].IsDraw = true;

        internal void Hide(int index)
            => _images[_indexOffset[index]].IsDraw = false;

        //internal void SetColor(int index, Vector3 color)
        //{
        //    _images[_indexOffset[index]].Color = color;
        //}

        internal void Update()
        {
            foreach (var image in _images)
            {
                image.Update();
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
