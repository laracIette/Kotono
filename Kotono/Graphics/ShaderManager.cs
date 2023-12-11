using Kotono.Graphics.Shaders;

namespace Kotono.Graphics
{
    public static class ShaderManager
    {        
        public static LightingShader Lighting { get; } = new();
        
        public static HitboxShader Hitbox { get; } = new();

        public static PointLightShader PointLight { get; } = new();
        
        public static ImageShader Image { get; } = new();
        
        public static GizmoShader Gizmo { get; } = new();

        public static RoundedBoxShader RoundedBox { get; } = new();
        
        public static RoundedBorderShader RoundedBorder { get; } = new();
        
        public static PostProcessShader PostProcessShader { get; } = new();

        public static void Init()
        {
            Lighting.Init();
            Hitbox.Init();
            PointLight.Init();
            Image.Init();
            Gizmo.Init();
            RoundedBox.Init();
            RoundedBorder.Init();

            PostProcessShader.Init();
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

            PostProcessShader.Update();
        }
    }
}
