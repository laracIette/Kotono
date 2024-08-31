// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal partial class RoundedBorderShader : Shader
    {
        private RoundedBorderShader() : base("roundedBorder") { }

        private static readonly global::System.Lazy<RoundedBorderShader> _instance = new(() => new());

        internal static RoundedBorderShader Instance => _instance.Value;

        internal override void SetVertexAttributesData() { }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetColor(global::Kotono.Utils.Color color) => SetColor("color", color);

        internal void SetSides(global::Kotono.Utils.Color sides) => SetColor("sides", sides);

        internal void SetFallOff(float fallOff) => SetFloat("fallOff", fallOff);

        internal void SetCornerSize(float cornerSize) => SetFloat("cornerSize", cornerSize);

        internal void SetThickness(float thickness) => SetFloat("thickness", thickness);
    }
}
