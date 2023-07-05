namespace Kotono.Graphics.Shaders
{
    internal class GizmoShader : Shader
    {
        internal GizmoShader() 
            : base("Graphics/Shaders/gizmo.vert", "Graphics/Shaders/gizmo.frag")
        { }

        internal override void Update()
        {
            SetMatrix4("view", KT.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", KT.ActiveCamera.ProjectionMatrix);
        }
    }
}
