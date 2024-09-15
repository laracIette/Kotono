// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class PainterShader : Shader
    {
        private PainterShader() : base("painter") { }

        private static readonly global::System.Lazy<PainterShader> _instance = new(() => new());

        internal static PainterShader Instance => _instance.Value;

        internal override void SetVertexAttributesLayout() { }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetTex(global::OpenTK.Graphics.OpenGL4.TextureUnit tex) => SetTextureUnit("tex", tex);
    }
}
