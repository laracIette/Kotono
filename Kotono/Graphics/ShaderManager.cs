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
        RoundedBox,
        RoundedBorder
    }

    public class ShaderManager
    {
        private readonly Shader[] _shaders =
        {
            new LightingShader(),
            new HitboxShader(),
            new PointLightShader(),
            new ImageShader(),
            new GizmoShader(),
            new RoundedBoxShader(),
            new RoundedBorderShader()
        };

        public ShaderManager() { }

        public void Init()
        {
            foreach (var shader in _shaders)
            {
                shader.Init();
            }
        }

        public int GetAttribLocation(ShaderType type, string attribName)
        {
            return _shaders[(int)type].GetAttribLocation(attribName);
        }

        public void SetBool(ShaderType type, string name, bool data)
        {
            _shaders[(int)type].SetBool(name, data);
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

        public void SetVector(ShaderType type, string name, Vector data)
        {
            _shaders[(int)type].SetVector(name, data);
        }

        public void SetColor(ShaderType type, string name, Color data)
        {
            _shaders[(int)type].SetColor(name, data);
        }

        public void SetRect(ShaderType type, string name, Rect data)
        {
            _shaders[(int)type].SetRect(name, data);
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
