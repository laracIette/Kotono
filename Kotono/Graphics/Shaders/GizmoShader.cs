namespace Kotono.Graphics.Shaders
{
    public class GizmoShader : Shader
    {
        public GizmoShader() 
            : base("Graphics/Shaders/gizmo.vert", "Graphics/Shaders/gizmo.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", KT.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", KT.ActiveCamera.ProjectionMatrix);
        }
    }
}
