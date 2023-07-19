using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    public class FrontMeshManager : DrawableManager
    {
        public FrontMeshManager()
            : base() { }

        public override void Draw()
        {
            GL.Clear(ClearBufferMask.DepthBufferBit);

            base.Draw();
        }
    }
}
