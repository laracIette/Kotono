using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class CameraManager
    {
        private readonly List<Camera> _cameras = new();
        
        public CameraManager() 
        { 
            _cameras.Add(new Camera());
        }

        public int Create(Camera camera)
        {
            _cameras.Add(camera);

            return _cameras.Count - 1;
        }

        public Vector3 GetPosition(int index)
            => _cameras[index].Position;

        public Matrix4 GetViewMatrix(int index)
            => _cameras[index].ViewMatrix;

        public Matrix4 GetProjectionMatrix(int index) 
            => _cameras[index].ProjectionMatrix;

        public Vector3 GetFront(int index)
            => _cameras[index].Front;

        public void SetAspectRatio(int index, float aspectRatio)
        {
            _cameras[index].AspectRatio = aspectRatio;
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
