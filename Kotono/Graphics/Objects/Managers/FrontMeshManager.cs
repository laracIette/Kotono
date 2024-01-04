using Kotono.Graphics.Objects.Meshes;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    internal class FrontMeshManager()
        : DrawableManager<FrontMesh>()
    {
        internal override void Draw()
        {
            //GL.Clear(ClearBufferMask.DepthBufferBit); // TODO: that, works but problem with outline

            base.Draw();
        }
    }
}
