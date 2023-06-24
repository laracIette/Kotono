using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDisposable
    {
        private Vector _ambient;

        protected Vector _diffuse;

        private Vector _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly IMesh _mesh;

        public PointLight(Vector position, Vector ambient, Vector diffuse, Vector specular, float constant, float linear, float quadratic)
        {
            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _constant = constant;
            _linear = linear;
            _quadratic = quadratic;
            _shaderIndex = KT.GetPointLightsCount();

            _mesh = KT.CreateMesh(new PointLightMesh(position));
        }

        public virtual void Update()
        {
            _mesh.Color = _diffuse;
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void UpdateShaders()
        {
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].position", _mesh.Position);
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].ambient", _ambient);
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].diffuse", _diffuse);
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].specular", _specular);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].constant", _constant);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].linear", _linear);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].quadratic", _quadratic);
        }

        public void Draw()
        {
            
        }

        public void Dispose()
        {
            KT.DeleteMesh(_mesh);

            GC.SuppressFinalize(this);
        }

        public bool IsGravity
        {
            get => _mesh.IsGravity;
            set => _mesh.IsGravity = value;
        } 
    }
}