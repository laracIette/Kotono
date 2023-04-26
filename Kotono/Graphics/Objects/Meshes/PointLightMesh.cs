using Kotono.Graphics.Objects.Hitboxes;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : MeshOBJ
    {
        public PointLightMesh(Vector3 position) 
            : base(
                  @"assets/sphere.obj",
                  position,
                  Vector3.Zero,
                  new Vector3(0.2f),
                  @"assets/white.png",
                  @"assets/white.png",
                  ShaderType.PointLight,
                  Vector3.One,
                  new int[]
                  {
                      KT.CreateHitbox(new Sphere())
                  }
              )
        {
        }

        public override void Draw()
        {
            KT.SetShaderMatrix4(_shaderType, "model", Model);
            KT.SetShaderVector3(_shaderType, "color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
