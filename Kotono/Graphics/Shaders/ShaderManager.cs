using Kotono.Graphics.Shaders;
using Kotono.Utils.Exceptions;

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

        internal static Shader Get(string name)
        {
            return name switch
            {
                "lighting" => Lighting,
                "hitbox" => Hitbox,
                "pointLight" => PointLight,
                "image" => Image,
                "gizmo" => Gizmo,
                "roundedBox" => RoundedBox,
                "roundedBorder" => RoundedBorder,
                "color" => Color,
                "blur" => Blur,
                "outline" => Outline,
                "flatTexture" => FlatTexture,
                _ => throw new SwitchException(typeof(string), name)
            };
        }
    }
}
