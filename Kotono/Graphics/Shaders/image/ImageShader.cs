// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class ImageShader : Shader
    {
        private ImageShader() : base("image") { }

        private static readonly global::System.Lazy<ImageShader> _instance = new(() => new());

        internal static ImageShader Instance => _instance.Value;

        internal override void SetVertexAttributesData() { }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetTexSampler(int texSampler) => SetInt("texSampler", texSampler);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
