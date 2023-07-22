using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Shaders
{
    public class GizmoShader : Shader
    {
        public GizmoShader() 
            : base("Graphics/Shaders/gizmo.vert", "Graphics/Shaders/gizmo.frag")
        { }

        public override void Update()
        {
            SetMatrix4("view", CameraManager.ActiveCamera.ViewMatrix);
            SetMatrix4("projection", CameraManager.ActiveCamera.ProjectionMatrix);
        }
    }
}
