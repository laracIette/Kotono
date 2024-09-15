// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class GizmoShader : Shader
    {
        private GizmoShader() : base("gizmo") { }

        private static readonly global::System.Lazy<GizmoShader> _instance = new(() => new());

        internal static GizmoShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 0);

        private static void SetANormal() => SetVertexAttributeData(1, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 12);

        private static void SetATangent() => SetVertexAttributeData(2, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 24);

        private static void SetATexCoords() => SetVertexAttributeData(3, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 36);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetANormal(); SetATangent(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetView(global::OpenTK.Mathematics.Matrix4 view) => SetMatrix4("view", view);

        internal void SetProjection(global::OpenTK.Mathematics.Matrix4 projection) => SetMatrix4("projection", projection);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);
    }
}
