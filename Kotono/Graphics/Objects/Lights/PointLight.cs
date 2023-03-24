using Kotono.Graphics.Objects.Meshes;
using Kotono.Graphics.Shaders;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight
    {
        private Vector3 _ambient;

        protected Vector3 _diffuse;

        private Vector3 _specular;

        private float _constant;

        private float _linear;

        private float _quadratic;

        private int _shaderIndex;

        private readonly PointLightMesh _mesh;

        private int _lightingShader;

        public PointLight(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
        {
            _ambient = ambient;
            _diffuse = diffuse;
            _specular = specular;
            _constant = constant;
            _linear = linear;
            _quadratic = quadratic;
            _shaderIndex = KT.GetPointLightsCount();

            _mesh = new PointLightMesh(position);

            _lightingShader = KT.CreateShader(new LightingShader());
        }

        public virtual void Update()
        {
            _mesh.Update();
            _mesh.Color = _diffuse;
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void UpdateShaders()
        {
            KT.SetShaderVector3(_lightingShader, $"pointLights[{_shaderIndex}].position", _mesh.Position);
            KT.SetShaderVector3(_lightingShader, $"pointLights[{_shaderIndex}].ambient", _ambient);
            KT.SetShaderVector3(_lightingShader, $"pointLights[{_shaderIndex}].diffuse", _diffuse);
            KT.SetShaderVector3(_lightingShader, $"pointLights[{_shaderIndex}].specular", _specular);
            KT.SetShaderFloat(_lightingShader, $"pointLights[{_shaderIndex}].constant", _constant);
            KT.SetShaderFloat(_lightingShader, $"pointLights[{_shaderIndex}].linear", _linear);
            KT.SetShaderFloat(_lightingShader, $"pointLights[{_shaderIndex}].quadratic", _quadratic);
        }

        public void Draw()
        {
            _mesh.Draw();
        }
    }
}