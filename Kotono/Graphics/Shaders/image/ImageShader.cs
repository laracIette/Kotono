// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class ImageShader : Shader
    {
        private ImageShader() : base("image") { }

        private static readonly global::System.Lazy<ImageShader> _instance = new(() => new());

        internal static ImageShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 12);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetTexSampler(global::OpenTK.Graphics.OpenGL4.TextureUnit texSampler) => SetTextureUnit("texSampler", texSampler);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
