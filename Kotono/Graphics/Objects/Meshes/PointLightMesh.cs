using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using OpenTK.Graphics.OpenGL4;
using System;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : Mesh
    {
        private readonly PointLight _pointLight;

        public PointLightMesh(Vector location, PointLight pointLight) 
            : base(
                  Path.Kotono + "Assets/Meshes/pointLight.ktf",
                  new IHitbox[]
                  {
                      new Sphere()
                  }
              )
        {
            Location = location;
            _pointLight = pointLight;
        }

        public override void Update()
        {
            base.Update();

            Color = _pointLight.Color;
        }

        public override void Draw()
        {
            _shader.SetMatrix4("model", Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
