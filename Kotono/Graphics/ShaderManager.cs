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
        Image
    }

    internal class ShaderManager
    {
        private readonly List<Shader> _shaders = new();

        internal ShaderManager() { }

        internal void Init()
        {
            _shaders.Add(new LightingShader());
            _shaders.Add(new HitboxShader());
            _shaders.Add(new PointLightShader());
            _shaders.Add(new ImageShader());
        }

        internal int GetAttribLocation(ShaderType type, string attribName)
        {
            return _shaders[(int)type].GetAttribLocation(attribName);
        }

        internal void SetInt(ShaderType type, string name, int data)
        {
            _shaders[(int)type].SetInt(name, data);
        }

        internal void SetFloat(ShaderType type, string name, float data)
        {
            _shaders[(int)type].SetFloat(name, data);
        }

        internal void SetMatrix4(ShaderType type, string name, Matrix4 data)
        {
            _shaders[(int)type].SetMatrix4(name, data);
        }

        internal void SetVector2(ShaderType type, string name, Vector2 data)
        {
            _shaders[(int)type].SetVector2(name, data);
        }

        internal void SetVector3(ShaderType type, string name, Vector3 data)
        {
            _shaders[(int)type].SetVector3(name, data);
        }

        internal void SetVector4(ShaderType type, string name, Vector4 data)
        {
            _shaders[(int)type].SetVector4(name, data);
        }

        internal void Update()
        {
            foreach (var shader in _shaders)
            {
                shader.Update();
            }
        }
    }
}
