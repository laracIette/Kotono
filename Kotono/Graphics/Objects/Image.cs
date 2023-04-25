﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects
{
    public class Image
    {
        private static readonly float[] _vertices =
        {           
            // positions   // texCoords
            -1.0f,  1.0f,  0.0f, 1.0f, 
            -1.0f, -1.0f,  0.0f, 0.0f, 
             1.0f, -1.0f,  1.0f, 0.0f, 

            -1.0f,  1.0f,  0.0f, 1.0f, 
             1.0f, -1.0f,  1.0f, 0.0f, 
             1.0f,  1.0f,  1.0f, 1.0f  
        }; 

        private static int _vertexArrayObject; 

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        private readonly Rect _dest;

        private readonly int _texture;

        private Matrix4 Model => Matrix4.Identity;

        public Image(string path, Rect dest) 
        {
            if (_isFirst)
            {
                _isFirst = false;

                // create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _vertices.Length, _vertices, BufferUsageHint.StaticDraw);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            }

            _dest = dest;

            _texture = TextureManager.LoadTexture(path);
        }

        public void Draw()
        {
            TextureManager.UseTexture(_texture, TextureUnit.Texture0);
            
            KT.SetShaderMatrix4(ShaderType.Image, "model", Model);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}