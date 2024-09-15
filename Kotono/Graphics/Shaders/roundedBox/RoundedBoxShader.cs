// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class RoundedBoxShader : Shader
    {
        private RoundedBoxShader() : base("roundedBox") { }

        private static readonly global::System.Lazy<RoundedBoxShader> _instance = new(() => new());

        internal static RoundedBoxShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 20, 12);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);

        internal void SetSides(global::Kotono.Utils.Coordinates.Sides sides) => SetSides("sides", sides);

        internal void SetFallOff(float fallOff) => SetFloat("fallOff", fallOff);

        internal void SetCornerSize(float cornerSize) => SetFloat("cornerSize", cornerSize);
    }
}
