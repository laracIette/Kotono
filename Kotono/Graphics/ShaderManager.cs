using Kotono.Graphics.Shaders;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    public enum ShaderType
    {
        Lighting,
        Hitbox,
        PointLight,
        Sphere,
        Image
    }

    public class ShaderManager
    {
        private readonly List<Shader> _shaders = new();

        public ShaderManager() 
        {
        }

        public void Init()
        {
            _shaders.Add(new LightingShader());
            _shaders.Add(new HitboxShader());
            _shaders.Add(new PointLightShader());
            _shaders.Add(new SphereShader());
            _shaders.Add(new ImageShader());
        }

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

        public void SetVector2(ShaderType type, string name, Vector2 data)
        {
            _shaders[(int)type].SetVector2(name, data);
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
