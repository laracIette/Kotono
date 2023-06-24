using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : MeshOBJ
    {
        public PointLightMesh(Vector position) 
            : base(
                  KT.ProjectPath + @"Assets/sphere.obj",
                  position,
                  Vector.Zero,
                  new Vector(0.2f),
                  KT.ProjectPath + @"Assets/white.png",
                  KT.ProjectPath + @"Assets/white.png",
                  ShaderType.PointLight,
                  Vector.One,
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
            KT.SetShaderVector(_shaderType, "color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
