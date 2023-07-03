using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : Mesh
    {
        public PointLightMesh() 
            : base(
                  KT.KotonoPath + "Assets/Meshes/pointLight.ktf",
                  new IHitbox[]
                  {
                      KT.CreateHitbox(new Sphere())
                  }
              )
        {
        }

        public override void Draw()
        {
            KT.SetShaderMatrix4(_shaderType, "model", Model);
            KT.SetShaderColor(_shaderType, "color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
