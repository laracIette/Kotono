using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDrawable
    {
        public bool IsDraw { get; set; }

        public bool IsGravity
        {
            get => _mesh.IsGravity;
            set => _mesh.IsGravity = value;
        }

        public Color Color { get; protected set; }
       
        public Color Ambient { get; protected set; }

        public Color Specular { get; protected set; }

        public float Constant { get; protected set; }

        public float Linear { get; protected set; }

        public float Quadratic { get; protected set; }

        private int _shaderIndex;

        private readonly Mesh _mesh;

        public const int MAX_COUNT = 100;

        public static int Count { get; internal set; }

        public PointLight(Vector location, Color ambient, Color diffuse, Color specular, float constant, float linear, float quadratic)
        {
            Ambient = ambient;
            Color = diffuse;
            Specular = specular;
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
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
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].ambient", Ambient);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].diffuse", Color);
            KT.SetShaderColor(ShaderType.Lighting, $"pointLights[{_shaderIndex}].specular", Specular);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].constant", Constant);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].linear", Linear);
            KT.SetShaderFloat(ShaderType.Lighting, $"pointLights[{_shaderIndex}].quadratic", Quadratic);
        }

        public void Draw()
        {
            
        }

        public void Save()
        {

        }

        public void Dispose()
        {
            KT.DeleteMesh(_mesh);

            Count--;

            GC.SuppressFinalize(this);
        }
    }
}