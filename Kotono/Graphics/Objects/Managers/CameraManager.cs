using System.Collections.Generic;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class CameraManager
    {
        private static readonly List<Camera> _cameras = new();

        internal static Camera ActiveCamera => _cameras[0];

        internal static void Create(Camera camera)
        {
            if (!_cameras.Contains(camera))
            {
                _cameras.Add(camera);
            }
        }

        internal static void Delete(Camera camera)
        {
            _cameras.Remove(camera);
        }

        internal static Camera Get(int index)
        {
            return _cameras[index];
        }

        internal static void Update()
        {
            foreach (var camera in _cameras)
            {
                camera.Update();
            }
        }
    }
}
