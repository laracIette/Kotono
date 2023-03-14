using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : MeshOBJ
    {
        public PointLightMesh(Vector3 position) 
            : base(
                  "sphere.obj",
                  position,
                  Vector3.Zero,
                  new Vector3(0.2f),
                  "white.png",
                  "white.png",
                  ShaderManager.PointLight,
                  Vector3.One
              )
        {
        }

        public override void Draw()
        {
            ShaderManager.PointLight.SetMatrix4("model", Model);
            ShaderManager.PointLight.SetVector3("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
