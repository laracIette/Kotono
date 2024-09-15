// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class ColorShader : Shader
    {
        private ColorShader() : base("color") { }

        private static readonly global::System.Lazy<ColorShader> _instance = new(() => new());

        internal static ColorShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 12);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetATexCoords(); }

        internal void SetColor(global::OpenTK.Graphics.OpenGL4.TextureUnit color) => SetTextureUnit("color", color);
    }
}
