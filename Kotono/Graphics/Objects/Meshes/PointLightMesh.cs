using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

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
                  new PointLightShader(),
                  Vector3.One
              )
        {
        }

        public override void Draw()
        {
            KT.SetShaderMatrix4(_shader, "model", Model);
            KT.SetShaderVector3(_shader, "color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
