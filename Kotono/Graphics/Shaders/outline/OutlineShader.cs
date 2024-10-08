// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class OutlineShader : Shader
    {
        private OutlineShader() : base("outline") { }

        private static readonly global::System.Lazy<OutlineShader> _instance = new(() => new());

        internal static OutlineShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 0);

        private static void SetATexCoords() => SetVertexAttributeData(1, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 16, 8);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetATexCoords(); }

        internal void SetDepth(global::OpenTK.Graphics.OpenGL4.TextureUnit depth) => SetTextureUnit("depth", depth);
    }
}
