﻿using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Kotono.Graphics.Objects.Meshes
{
    public class PointLightMesh : Mesh
    {
        public PointLightMesh(Vector location) 
            : base(
                  KT.ProjectPath + @"Assets/Meshes/sphere.obj",
                  new Transform
                  {
                      Location = location,
                      Rotation = Vector.Zero,
                      Scale = new Vector(0.2f)
                  },
                  new string[] { KT.ProjectPath + @"Assets/white.png" },
                  ShaderType.PointLight,
                  Color.White,
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
