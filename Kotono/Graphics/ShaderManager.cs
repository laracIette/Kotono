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

    public static class ShaderManager
    {
        private static readonly Shader[] _shaders =
        {
            new LightingShader(),
            new HitboxShader(),
            new PointLightShader(),
            new ImageShader(),
            new GizmoShader(),
            new RoundedBoxShader(),
            new RoundedBorderShader()
        };

        public static void Init()
        {
            foreach (var shader in _shaders)
            {
                shader.Init();
            }
        }

        public static int GetAttribLocation(ShaderType type, string attribName)
        {
            return _shaders[(int)type].GetAttribLocation(attribName);
        }

        public static void SetBool(ShaderType type, string name, bool data)
        {
            _shaders[(int)type].SetBool(name, data);
        }

        public static void SetInt(ShaderType type, string name, int data)
        {
            _shaders[(int)type].SetInt(name, data);
        }

        public static void SetFloat(ShaderType type, string name, float data)
        {
            _shaders[(int)type].SetFloat(name, data);
        }

        public static void SetMatrix4(ShaderType type, string name, Matrix4 data)
        {
            _shaders[(int)type].SetMatrix4(name, data);
        }

        public static void SetVector(ShaderType type, string name, Vector data)
        {
            _shaders[(int)type].SetVector(name, data);
        }

        public static void SetColor(ShaderType type, string name, Color data)
        {
            _shaders[(int)type].SetColor(name, data);
        }

        public static void SetRect(ShaderType type, string name, Rect data)
        {
            _shaders[(int)type].SetRect(name, data);
        }

        public static void Update()
        {
            foreach (var shader in _shaders)
            {
                shader.Update();
            }
        }
    }
}
