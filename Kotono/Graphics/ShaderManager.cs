using Kotono.Graphics.Shaders;
using Kotono.Utils;
using OpenTK.Mathematics;

namespace Kotono.Graphics
{
    public enum ShaderType
    {
        Lighting,
        Hitbox,
        PointLight,
        Image,
        Gizmo,
        RoundedBox
    }

    internal class ShaderManager
    {
        private Shader[] _shaders = new Shader[6];

        internal ShaderManager() { }

        internal void Init()
        {
            _shaders = new Shader[] {
                new LightingShader(),
                new HitboxShader(),
                new PointLightShader(),
                new ImageShader(),
                new GizmoShader(),
                new RoundedBoxShader()
            };
        }

        internal int GetAttribLocation(ShaderType type, string attribName)
        {
            return _shaders[(int)type].GetAttribLocation(attribName);
        }

        internal void SetBool(ShaderType type, string name, bool data)
        {
            _shaders[(int)type].SetBool(name, data);
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

        internal void SetVector(ShaderType type, string name, Vector data)
        {
            _shaders[(int)type].SetVector(name, data);
        }

        internal void SetColor(ShaderType type, string name, Color data)
        {
            _shaders[(int)type].SetColor(name, data);
        }

        internal void SetRect(ShaderType type, string name, Rect data)
        {
            _shaders[(int)type].SetRect(name, data);
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
