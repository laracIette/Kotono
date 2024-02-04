using Kotono.Graphics.Shaders;

namespace Kotono.Graphics
{
    internal static class ShaderManager
    {
        internal static LightingShader Lighting { get; } = new();

        internal static HitboxShader Hitbox { get; } = new();

        internal static PointLightShader PointLight { get; } = new();

        internal static ImageShader Image { get; } = new();

        internal static GizmoShader Gizmo { get; } = new();

        internal static RoundedBoxShader RoundedBox { get; } = new();

        internal static RoundedBorderShader RoundedBorder { get; } = new();

        internal static ColorShader Color { get; } = new();

        internal static BlurShader Blur { get; } = new();

        internal static OutlineShader Outline { get; } = new();

        internal static FlatTextureShader FlatTexture { get; } = new();

        internal static void Update()
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
            FlatTexture.Update();
        }
    }
}
