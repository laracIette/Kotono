// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class BlurShader : Shader
    {
        private BlurShader() : base("blur") { }

        private static readonly global::System.Lazy<BlurShader> _instance = new(() => new());

        internal static BlurShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 8);

        internal override void SetVertexAttributesData() { SetAPos(); SetATexCoords(); }

        internal void SetColor(int color) => SetInt("color", color);
    }
}
