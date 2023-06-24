using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight : IDisposable
    {
        private float _cutOffAngle = 12.5f;

        private float _outerCutOffAngle = 17.5f;

        private bool _isOn = true;

        private int _shaderIndex;

        private float CutOffAngle
        {
            get => _cutOffAngle;
            set => _cutOffAngle = MathHelper.Clamp(value, 0.0f, 12.5f);
        }

        private float OuterCutOffAngle
        {
            get => _outerCutOffAngle;
            set => _outerCutOffAngle = MathHelper.Clamp(value, 0.0f, 17.5f);
        }

        public SpotLight()
        {
            _shaderIndex = KT.GetSpotLightsCount();
        }

        public void Update()
        {
            if (InputManager.KeyboardState!.IsKeyPressed(Keys.F))
            {
                _isOn = !_isOn;
            }

            if (_isOn)
            {
                if (OuterCutOffAngle >= 5.0f)
                {
                    CutOffAngle += 100.0f * Time.DeltaS;
                }
                OuterCutOffAngle += 100.0f * Time.DeltaS;
            }
            else
            {
                CutOffAngle -= 100.0f * Time.DeltaS;
                OuterCutOffAngle -= 100.0f * Time.DeltaS;
            }
        }

        public void UpdateShaders()
        {
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].cutOff", MathF.Cos(MathHelper.DegreesToRadians(CutOffAngle)));
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(OuterCutOffAngle)));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].position", KT.GetCameraPosition(0));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].direction", KT.GetCameraFront(0));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].ambient", new Vector(0.0f, 0.0f, 0.0f));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].diffuse", new Vector(1.0f, 1.0f, 1.0f));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{ _shaderIndex}].specular", new Vector(1.0f, 1.0f, 1.0f));
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].constant", 1.0f);
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].linear", 0.09f);
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].quadratic", 0.032f);
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void Draw()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
