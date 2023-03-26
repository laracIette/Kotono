using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDisposable
    {
        private Vector3 _ambient;

        protected Vector3 _diffuse;

        private Vector3 _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly int _mesh;

        public PointLight(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
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
            KT.SetMeshColor(_mesh, _diffuse);
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void UpdateShaders()
        {
            KT.SetShaderVector3(ShaderType.Lighting, $"pointLights[{_shaderIndex}].position", KT.GetMeshPosition(_mesh));
            KT.SetShaderVector3(ShaderType.Lighting, $"pointLights[{_shaderIndex}].ambient", _ambient);
            KT.SetShaderVector3(ShaderType.Lighting, $"pointLights[{_shaderIndex}].diffuse", _diffuse);
            KT.SetShaderVector3(ShaderType.Lighting, $"pointLights[{_shaderIndex}].specular", _specular);
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
    }
}