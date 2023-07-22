using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    public static class CameraManager
    {
        private static readonly List<Camera> _cameras = new();

        public static Camera ActiveCamera => _cameras[0];

        public static Camera Create(Camera camera)
        {
            _cameras.Add(camera);
            return camera;
        }

        public static void Delete(Camera camera)
        {
            _cameras.Remove(camera);
        }

        public static Camera Get(int index)
        {
            return _cameras[index];
        }

        public static void Update()
        {
            foreach (var camera in _cameras)
            {
                camera.Update();
            }
        }
    }
}
