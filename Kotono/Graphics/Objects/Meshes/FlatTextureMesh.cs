using Kotono.Settings;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        internal FlatTextureMesh()
            : base(
                JsonParser.Parse<MeshSettings>(Path.ASSETS + @"Meshes\flatTextureMesh.json")
            )
        {
        }

        public override void Draw()
        {
            _textures[0].Use();

            _shader.SetMatrix4("model", Transform.Model);
            _shader.SetColor("color", Color);

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}
