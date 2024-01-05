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

        public static ColorShader Color { get; } = new();

        public static BlurShader Blur { get; } = new();

        public static OutlineShader Outline { get; } = new();

        public static void Update()
        {
            Lighting.Update();
            Hitbox.Update();
            PointLight.Update();
            Image.Update();
            Gizmo.Update();
            RoundedBox.Update();
            RoundedBorder.Update();
            Color.Update();
            Blur.Update();
            Outline.Update();
        }
    }
}
