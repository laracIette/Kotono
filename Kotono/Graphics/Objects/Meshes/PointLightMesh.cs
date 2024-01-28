using Kotono.File;
using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class PointLightMesh()
        : Mesh(
            Settings.Parse<MeshSettings>(Path.ASSETS + @"Meshes\pointLightMesh.json")
        )
    {
        private PointLight? _pointLight = null;

        internal void AttachTo(PointLight pointLight)
        {
            _pointLight = pointLight;
        }

        internal void Detach()
        {
            _pointLight = null;
        }

        public override void Update()
        {
            if (_pointLight == null)
            {
                IsDraw = false;
                return;
            }

            IsDraw = _pointLight.IsDraw;

            base.Update();

            Color = _pointLight.Color;
        }

        public override void Draw()
        {
            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
