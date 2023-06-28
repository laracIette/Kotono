using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class CameraManager
    {
        private readonly List<Camera> _cameras = new();

        internal CameraManager() { }

        internal void Create(Camera camera)
        {
            _cameras.Add(camera);
        }

        internal void Delete(Camera camera)
        {
            if (_cameras.Count <= 0)
            {
                KT.Print($"The number of Camera is already at 0.");
            }
            else
            {
                _cameras.Remove(camera);
            }
        }

        internal Camera Get(int index)
        {
            if (_cameras.Count <= 0)
            {
                throw new IndexOutOfRangeException($"The number of Camera is already at 0.");
            }
            else
            {
                return _cameras[index];
            }
        }

        internal void Update()
        {
            foreach (var camera in _cameras)
            {
                camera.Update();
            }
        }
    }
}
