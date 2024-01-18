using Kotono.File;
using Kotono.Utils;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        internal FlatTextureMesh()
            : base(
                Settings.Parse<MeshSettings>(Path.ASSETS + @"Meshes\flatTextureMesh.ktf")
            )
        {
        }

        //public override void Draw()
        //{
        //    _texture.Use();

        //    _shader.SetInt("tex", _texture.Handle);
        //    _shader.SetMatrix4("model", Transform.Model);
        //    _shader.SetColor("color", Color);

        //    GL.BindVertexArray(VertexArrayObject);
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

        //    GL.DrawElements(PrimitiveType.Triangles, IndicesCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        //}
    }
}
