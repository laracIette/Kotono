// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class Shape2DShader : Shader
    {
        private Shape2DShader() : base("shape2D") { }

        private static readonly global::System.Lazy<Shape2DShader> _instance = new(() => new());

        internal static Shape2DShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 8, 0);

        internal override void SetVertexAttributesData() { SetAPos(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
