using Kotono.Graphics.Shaders;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics
{
    public class ShaderManager
    {
        private readonly List<Shader> _shaders = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _shaderIndex = 0;

        public ShaderManager() { }

        public int Create(Shader shader)
        {
            foreach (var key in _indexOffset.Keys)
            {
                if (_shaders[_indexOffset[key]].ToString() == shader.ToString())
                {
                    return key;
                }
            }

            _indexOffset[_shaderIndex] = _shaders.Count;

            _shaders.Add(shader);

            return _shaderIndex++;
        }

        public void Delete(int index)
        {
            _shaders.RemoveAt(_indexOffset[index]);

            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
        }

        public int GetAttribLocation(int index, string attribName)
            => _shaders[_indexOffset[index]].GetAttribLocation(attribName);

        public void SetInt(int index, string name, int data)
        {
            _shaders[_indexOffset[index]].SetInt(name, data);
        }

        public void SetFloat(int index, string name, float data)
        {
            _shaders[_indexOffset[index]].SetFloat(name, data);
        }

        public void SetMatrix4(int index, string name, Matrix4 data)
        {
            _shaders[_indexOffset[index]].SetMatrix4(name, data);
        }

        public void SetVector3(int index, string name, Vector3 data)
        {
            _shaders[_indexOffset[index]].SetVector3(name, data);
        }

        public void SetVector4(int index, string name, Vector4 data)
        {
            _shaders[_indexOffset[index]].SetVector4(name, data);
        }

        public void Update()
        {
            foreach (var shader in _shaders)
            {
                shader.Update();
            }
        }
    }
}
