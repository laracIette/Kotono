// This file is auto-generated and any change will be overwritten on the next update.

namespace Kotono.Graphics.Shaders
{
    internal sealed partial class NewLightingShader : Shader
    {
        private NewLightingShader() : base("newLighting") { }

        private static readonly global::System.Lazy<NewLightingShader> _instance = new(() => new());

        internal static NewLightingShader Instance => _instance.Value;

        private static void SetAPos() => SetVertexAttributeData(0, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 0);

        private static void SetANormal() => SetVertexAttributeData(1, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 12);

        private static void SetATangent() => SetVertexAttributeData(2, 3, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 24);

        private static void SetATexCoords() => SetVertexAttributeData(3, 2, global::OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 44, 36);

        internal override void SetVertexAttributesLayout() { SetAPos(); SetANormal(); SetATangent(); SetATexCoords(); }

        internal void SetModel(global::OpenTK.Mathematics.Matrix4 model) => SetMatrix4("model", model);

        internal void SetView(global::OpenTK.Mathematics.Matrix4 view) => SetMatrix4("view", view);

        internal void SetProjection(global::OpenTK.Mathematics.Matrix4 projection) => SetMatrix4("projection", projection);

        internal void SetMaterial(global::Kotono.Graphics.Material material) => SetMaterial("material", material);

        internal void SetPointLights(global::Kotono.Graphics.Objects.Lights.PointLight[] pointLights) { for (int i = 0; i < pointLights.Length; i++) SetPointLight($"pointLights[{i}]", pointLights[i]); }

        internal void SetNumPointLights(int numPointLights) => SetInt("numPointLights", numPointLights);

        internal void SetDirLight(global::Kotono.Graphics.Objects.Lights.DirectionalLight dirLight) => SetDirectionalLight("dirLight", dirLight);

        internal void SetViewPos(global::Kotono.Utils.Coordinates.Vector viewPos) => SetVector("viewPos", viewPos);

        internal void SetBaseColor(global::Kotono.Utils.Color baseColor) => SetColor("baseColor", baseColor);
    }
}
