using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using IO = System.IO;

namespace Kotono.Graphics.Shaders
{
    internal abstract class Shader
    {
        private readonly int _handle;

        private readonly Dictionary<string, int> _uniformLocations = [];

        
        internal string Name { get; }

        internal Shader(string name)
        { 
            var shaderSource = IO.File.ReadAllText(Path.SHADERS + name + ".vert");
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = IO.File.ReadAllText(Path.SHADERS + name + ".frag");
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            _handle = GL.CreateProgram();

            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);

            LinkProgram(_handle);

            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(_handle, i, out _, out _);
                var location = GL.GetUniformLocation(_handle, key);
                _uniformLocations.Add(key, location);
            }

            Name = name;
        }

        internal virtual void Update()
        {
            Use();
        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occurred whilst linking Program({program}).\n\n{infoLog}");
            }
        }

        internal void Use()
        {
            GL.UseProgram(_handle);
        }

        internal int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_handle, attribName);
        }

        internal void SetBool(string name, bool data)
        {
            Use();
            GL.Uniform1(_uniformLocations[name], data ? 1 : 0);
        }

        internal void SetInt(string name, int data)
        {
            Use();
            GL.Uniform1(_uniformLocations[name], data);
        }

        internal void SetFloat(string name, float data)
        {
            Use();
            GL.Uniform1(_uniformLocations[name], data);
        }

        internal void SetMatrix4(string name, Matrix4 data)
        {
            Use();
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        internal void SetVector(string name, Vector data)
        {
            Use();
            GL.Uniform3(_uniformLocations[name], (Vector3)data);
        }

        internal void SetColor(string name, Color data)
        {
            Use();
            GL.Uniform4(_uniformLocations[name], (Vector4)data);
        }

        internal void SetRect(string name, Rect data)
        {
            Use();
            GL.Uniform4(_uniformLocations[name], (Vector4)data);
        }

        internal void SetPoint(string name, Point data)
        {
            Use();
            GL.Uniform2(_uniformLocations[name], (Vector2)data);
        }
    }
}