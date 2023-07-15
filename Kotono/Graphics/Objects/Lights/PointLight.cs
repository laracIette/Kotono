using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDisposable
    {
        public Color Diffuse { get; protected set; }
       
        private Color _ambient;

        private Color _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly Mesh _mesh;

        public const int MAX_COUNT = 100;

        public static int Count { get; internal set; }

        public PointLight(Vector location, Color ambient, Color diffuse, Color specular, float constant, float linear, float quadratic)
        {
            _ambient = ambient;
            Diffuse = diffuse;
            _specular = specular;
            _constant = constant;
            _linear = linear;
            _quadratic = quadratic;
            _shaderIndex = Count;

            _mesh = new PointLightMesh(location, this);
        }

        public void Init() { }

        public virtual void Update()
        {
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void UpdateShaders()
        {
            KT.SetShaderVector(ShaderType.Lighting, $"pointLights[{_shaderIndex}].location", _mesh.Location);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].ambient", _ambient);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].diffuse", Diffuse);
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

            Count--;

            GC.SuppressFinalize(this);
        }

        public bool IsGravity
        {
            get => _mesh.IsGravity;
            set => _mesh.IsGravity = value;
        } 
    }
}