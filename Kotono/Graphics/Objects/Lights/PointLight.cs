using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDisposable
    {
        private Color _ambient;

        protected Color _diffuse;

        private Color _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly Mesh _mesh;

        public PointLight(Vector location, Color ambient, Color diffuse, Color specular, float constant, float linear, float quadratic)
        {
            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _constant = constant;
            _linear = linear;
            _quadratic = quadratic;
            _shaderIndex = KT.GetPointLightsCount();

            _mesh = KT.CreateMesh(new PointLightMesh(location));
        }

        public void Init() { }

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
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].location", _mesh.Location);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].ambient", _ambient);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].diffuse", _diffuse);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].specular", _specular);
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