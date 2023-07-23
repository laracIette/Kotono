using Kotono.Graphics.Shaders;
using Kotono.Utils;
using OpenTK.Mathematics;

namespace Kotono.Graphics
{
    public static class ShaderManager
    {
        private static readonly LightingShader _lightingShader = new();

        private static readonly HitboxShader _hitboxShader = new();
        
        private static readonly PointLightShader _pointLightShader = new();
        
        private static readonly ImageShader _imageShader = new();

        private static readonly GizmoShader _gizmoShader = new();
        
        private static readonly RoundedBoxShader _roundedBoxShader = new();
        
        private static readonly RoundedBorderShader _roundedBorderShader = new();
        
        public static LightingShader Lighting => _lightingShader;
        
        public static HitboxShader Hitbox => _hitboxShader;

        public static PointLightShader PointLight => _pointLightShader;
        
        public static ImageShader Image => _imageShader;
        
        public static GizmoShader Gizmo => _gizmoShader;

        public static RoundedBoxShader RoundedBox => _roundedBoxShader;
        
        public static RoundedBorderShader RoundedBorder => _roundedBorderShader;

        public static void Init()
        {
            Lighting.Init();
            Hitbox.Init();
            PointLight.Init();
            Image.Init();
            Gizmo.Init();
            RoundedBox.Init();
            RoundedBorder.Init();
        }

        public static void Update()
        {
            Lighting.Update();
            Hitbox.Update();
            PointLight.Update();
            Image.Update();
            Gizmo.Update();
            RoundedBox.Update();
            RoundedBorder.Update();
        }
    }
}
