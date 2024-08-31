// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class PointLightShader : Shader
    {
        private PointLightShader() : base("pointLight") { }

        private static readonly global::System.Lazy<PointLightShader> _instance = new(() => new());

        internal static PointLightShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 32, 0);

        private static void SetANormal() => SetVertexAttributeData(1, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 32, 12);

        private static void SetATexCoords() => SetVertexAttributeData(2, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 32, 24);

        internal override void SetVertexAttributesData() { SetAPos(); SetANormal(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetView(global::OpenTK.Mathematics.Matrix4 view) => SetMatrix4("view", view);

        internal void SetProjection(global::OpenTK.Mathematics.Matrix4 projection) => SetMatrix4("projection", projection);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
