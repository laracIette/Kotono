﻿using Kotono.Graphics.Objects.Hitboxes;
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
                  KT.KotonoPath + "Assets/Meshes/pointLight.ktf",
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

            Color = _pointLight.Diffuse;
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
