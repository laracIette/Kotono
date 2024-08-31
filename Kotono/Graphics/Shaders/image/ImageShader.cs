// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class ImageShader : Shader
    {
        private ImageShader() : base("image") { }

        private static readonly global::System.Lazy<ImageShader> _instance = new(() => new());

        internal static ImageShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 8);

        internal override void SetVertexAttributesData() { SetAPos(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetTexSampler(int texSampler) => SetInt("texSampler", texSampler);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
