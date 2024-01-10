﻿using Kotono.Graphics.Objects.Settings;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class FlatTextureMesh : Mesh
    {
        internal FlatTextureMesh() 
            : base(
                new MeshSettings
                {
                    Path = Path.ASSETS + @"Meshes\flatTextureMesh.ktf"
                }
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
