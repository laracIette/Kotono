using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class CameraManager
    {
        private readonly List<Camera> _cameras = new();

        public CameraManager() { }

        public void Create(Camera camera)
        {
            _cameras.Add(camera);
        }

        public void Delete(Camera camera)
        {
            _cameras.Remove(camera);
        }

        public Camera Get(int index)
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

        public void Update()
        {
            foreach (var camera in _cameras)
            {
                camera.Update();
            }
        }
    }
}
