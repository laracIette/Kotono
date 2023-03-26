using Kotono.Graphics.Shaders;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public enum ShaderType
    {
        Lighting,
        Hitbox,
        PointLight
    }

    public class ShaderManager
    {
        private readonly List<Shader> _shaders = new();

        public ShaderManager() 
        {
            _shaders.Add(new LightingShader());
            _shaders.Add(new HitboxShader());
            _shaders.Add(new PointLightShader());
        }

        /*
        
        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _shaderIndex = 0;
         
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
            if (_shaders.Count <= 0)
            {
                throw new Exception($"The number of Shader is already at 0.");
            }

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
        */

        public int GetAttribLocation(ShaderType type, string attribName)
        {
            return _shaders[(int)type].GetAttribLocation(attribName);
        }

        public void SetInt(ShaderType type, string name, int data)
        {
            _shaders[(int)type].SetInt(name, data);
        }

        public void SetFloat(ShaderType type, string name, float data)
        {
            _shaders[(int)type].SetFloat(name, data);
        }

        public void SetMatrix4(ShaderType type, string name, Matrix4 data)
        {
            _shaders[(int)type].SetMatrix4(name, data);
        }

        public void SetVector3(ShaderType type, string name, Vector3 data)
        {
            _shaders[(int)type].SetVector3(name, data);
        }

        public void SetVector4(ShaderType type, string name, Vector4 data)
        {
            _shaders[(int)type].SetVector4(name, data);
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
