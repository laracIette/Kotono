// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class HitboxShader : Shader
    {
        private HitboxShader() : base("hitbox") { }

        private static readonly global::System.Lazy<HitboxShader> _instance = new(() => new());

        internal static HitboxShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 12, 0);

        internal override void SetVertexAttributesData() { SetAPos(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetView(global::OpenTK.Mathematics.Matrix4 view) => SetMatrix4("view", view);

        internal void SetProjection(global::OpenTK.Mathematics.Matrix4 projection) => SetMatrix4("projection", projection);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
