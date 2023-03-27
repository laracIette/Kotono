using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public class CameraManager
    {
        private readonly List<Camera> _cameras = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _cameraIndex = 0;

        public CameraManager() 
        {
        }

        public int Create(Camera camera)
        {
            _indexOffset[_cameraIndex] = _cameras.Count;

            _cameras.Add(camera);

            return _cameraIndex++;
        }

        public void Delete(int index)
        {
            if (_cameras.Count <= 0)
            {
                throw new Exception($"The number of Camera is already at 0.");
            }

            _cameras.RemoveAt(_indexOffset[index]);

            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
        }

        public Vector3 GetPosition(int index)
            => _cameras[_indexOffset[index]].Position;

        public Matrix4 GetViewMatrix(int index)
            => _cameras[_indexOffset[index]].ViewMatrix;

        public Matrix4 GetProjectionMatrix(int index) 
            => _cameras[_indexOffset[index]].ProjectionMatrix;

        public Vector3 GetFront(int index)
            => _cameras[_indexOffset[index]].Front;
        
        public Vector3 GetUp(int index)
            => _cameras[_indexOffset[index]].Up;

        public Vector3 GetRight(int index)
            => _cameras[_indexOffset[index]].Right;

        public void SetAspectRatio(int index, float aspectRatio)
        {
            _cameras[_indexOffset[index]].AspectRatio = aspectRatio;
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
