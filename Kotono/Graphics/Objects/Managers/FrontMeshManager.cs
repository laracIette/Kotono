using Kotono.Graphics.Objects.Meshes;

namespace Kotono.Graphics.Objects.Managers
{
    internal class FrontMeshManager()
        : DrawableManager<FrontMesh>()
    {
        internal override void Draw()
        {
            //GL.Clear(ClearBufferMask.DepthBufferBit); // TODO: that

            base.Draw();
        }
    }
}
