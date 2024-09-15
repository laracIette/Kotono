// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class CubemapShader : Shader
    {
        private CubemapShader() : base("cubemap") { }

        private static readonly global::System.Lazy<CubemapShader> _instance = new(() => new());

        internal static CubemapShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 12, 0);

        internal override void SetVertexAttributesLayout() { SetAPos(); }

        internal void SetProjection(global::OpenTK.Mathematics.Matrix4 projection) => SetMatrix4("projection", projection);

        internal void SetView(global::OpenTK.Mathematics.Matrix4 view) => SetMatrix4("view", view);

        internal void SetSkybox(global::OpenTK.Graphics.OpenGL4.TextureUnit skybox) => SetTextureUnit("skybox", skybox);
    }
}
