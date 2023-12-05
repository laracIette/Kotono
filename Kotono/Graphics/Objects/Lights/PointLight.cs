using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight : IDrawable
    {
        public bool IsDraw { get; private set; }

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

        // temporary, TODO: get rid of that
        public static PointLight First => ObjectManager.GetFirstPointLight();

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

            ObjectManager.Create(this);
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
            ShaderManager.Lighting.SetVector($"pointLights[{_shaderIndex}].location", _mesh.Location);
            ShaderManager.Lighting.SetColor($"pointLights[{_shaderIndex}].ambient", Ambient);
            ShaderManager.Lighting.SetColor($"pointLights[{_shaderIndex}].diffuse", Color);
            ShaderManager.Lighting.SetColor($"pointLights[{_shaderIndex}].specular", Specular);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].constant", Constant);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].linear", Linear);
            ShaderManager.Lighting.SetFloat($"pointLights[{_shaderIndex}].quadratic", Quadratic);
        }

        public void Draw()
        {
            
        }

        public void Save()
        {

        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            _mesh.Delete();

            Count--;

            GC.SuppressFinalize(this);
        }
    }
}