using Kotono.Graphics.Objects.Meshes;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    public class FrontMeshManager : DrawableManager<FrontMesh>
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
