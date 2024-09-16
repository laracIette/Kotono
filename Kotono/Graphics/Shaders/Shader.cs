using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
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
            var shaderSource = IO.File.ReadAllText(Path.FromShaders($@"{name}\{name}.vert"));
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = IO.File.ReadAllText(Path.FromShaders($@"{name}\{name}.frag"));
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

            ShaderManager.Create(this);
        }

        internal abstract void SetVertexAttributesLayout();

        internal string[] GetVertexAttributes()
        {
            GL.GetProgram(_handle, GetProgramParameterName.ActiveAttributes, out int numAttributes);
            GL.GetProgram(_handle, GetProgramParameterName.ActiveAttributeMaxLength, out int maxNameLength);

            var attributes = new string[numAttributes];
            for (int i = 0; i < numAttributes; ++i)
            {
                GL.GetActiveAttrib(_handle, i, maxNameLength, out _, out _, out _, out string name);
                attributes[i] = name;
            }

            return attributes;
        }

        internal string[] GetUniforms()
        {
            return [.. _uniformLocations.Keys];
        }

        internal virtual void Update() { }

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

        internal void Use() => GL.UseProgram(_handle);

        private bool TryGetUniformLocation(in string name, out int location)
        {
            if (_uniformLocations.TryGetValue(name, out location))
            {
                Use();
                return true;
            }

            //Logger.LogError($"couldn't find attribute location '{name}' in Shader '{Name}'");
            return false;
        }

        internal void SetBool(in string name, bool data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform1(location, data ? 1 : 0);
            }
        }

        internal void SetInt(in string name, int data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform1(location, data);
            }
        }

        internal void SetFloat(in string name, float data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform1(location, data);
            }
        }

        internal void SetMatrix4(in string name, Matrix4 data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.UniformMatrix4(location, true, ref data);
            }
        }

        internal void SetVector(in string name, Vector data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform3(location, data.X, data.Y, data.Z);
            }
        }

        internal void SetColor(in string name, Color data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform4(location, data.R, data.G, data.B, data.A);
            }
        }

        internal void SetRect(in string name, Rect data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform4(location, data.RelativePosition.X, data.RelativePosition.Y, data.RelativeSize.X, data.RelativeSize.Y);
            }
        }

        internal void SetSides(in string name, Sides data)
        {
            SetFloat($"{name}.left", data.Left);
            SetFloat($"{name}.right", data.Right);
            SetFloat($"{name}.top", data.Top);
            SetFloat($"{name}.bottom", data.Bottom);
        }

        internal void SetPoint(in string name, Point data)
        {
            if (TryGetUniformLocation(name, out int location))
            {
                GL.Uniform2(location, data.X, data.Y);
            }
        }

        internal void SetTextureUnit(in string name, TextureUnit data)
        {
            SetInt(name, data - TextureUnit.Texture0);
        }

        internal void SetMaterial(in string name, Material data)
        {
            SetTextureUnit($"{name}.albedo", data.Albedo.TextureUnit);
            SetTextureUnit($"{name}.normal", data.Normal.TextureUnit);
            SetTextureUnit($"{name}.metallic", data.Metallic.TextureUnit);
            SetTextureUnit($"{name}.roughness", data.Roughness.TextureUnit);
            SetTextureUnit($"{name}.ambientOcclusion", data.AmbientOcclusion.TextureUnit);
            SetTextureUnit($"{name}.emissive", data.Emissive.TextureUnit);
        }

        internal void SetDirectionalLight(in string name, DirectionalLight data)
        {
            SetVector($"{name}.direction", data.Direction);
            SetColor($"{name}.ambient", data.Ambient);
            SetColor($"{name}.diffuse", data.Diffuse);
            SetColor($"{name}.specular", data.Specular);
            SetFloat($"{name}.intensity", data.Intensity);
        }

        internal void SetPointLight(in string name, PointLight data)
        {
            SetColor($"{name}.ambient", data.Ambient);
            SetColor($"{name}.diffuse", data.Diffuse);
            SetColor($"{name}.specular", data.Specular);
            SetFloat($"{name}.constant", data.Constant);
            SetFloat($"{name}.linear", data.Linear);
            SetFloat($"{name}.quadratic", data.Quadratic);
            SetFloat($"{name}.intensity", data.Intensity);
        }

        internal void SetSpotLight(in string name, SpotLight data)
        {
            SetColor($"{name}.ambient", data.Ambient);
            SetColor($"{name}.diffuse", data.Diffuse);
            SetColor($"{name}.specular", data.Specular);
            SetFloat($"{name}.constant", data.Constant);
            SetFloat($"{name}.linear", data.Linear);
            SetFloat($"{name}.quadratic", data.Quadratic);
            SetFloat($"{name}.cutOffAngle", data.CutOffAngle);
            SetFloat($"{name}.outerCutOffAngle", data.OuterCutOffAngle);
            SetFloat($"{name}.intensity", data.Intensity);
        }

        internal static void SetVertexAttributeData(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
        }

        public override string ToString() => $"Name: {Name}";
    }
}