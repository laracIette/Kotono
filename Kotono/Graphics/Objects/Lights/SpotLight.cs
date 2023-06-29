using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Kotono.Utils;
using System;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight : IDisposable
    {
        private float _cutOffRotation = 12.5f;

        private float _outerCutOffRotation = 17.5f;

        private bool _isOn = true;

        private int _shaderIndex;

        private float CutOffRotation
        {
            get => _cutOffRotation;
            set => _cutOffRotation = Math.Clamp(value, 0.0f, 12.5f);
        }

        private float OuterCutOffRotation
        {
            get => _outerCutOffRotation;
            set => _outerCutOffRotation = Math.Clamp(value, 0.0f, 17.5f);
        }

        public SpotLight()
        {
            _shaderIndex = KT.GetSpotLightsCount();
        }

        public void Init() { }

        public void Update()
        {
            if (Input.KeyboardState!.IsKeyPressed(Keys.F))
            {
                _isOn = !_isOn;
            }

            if (_isOn)
            {
                if (OuterCutOffRotation >= 5.0f)
                {
                    CutOffRotation += 100.0f * Time.DeltaS;
                }
                OuterCutOffRotation += 100.0f * Time.DeltaS;
            }
            else
            {
                CutOffRotation -= 100.0f * Time.DeltaS;
                OuterCutOffRotation -= 100.0f * Time.DeltaS;
            }
        }

        public void UpdateShaders()
        {
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].cutOff", MathF.Cos(MathHelper.DegreesToRadians(CutOffRotation)));
            KT.SetShaderFloat(ShaderType.Lighting, $"spotLights[{_shaderIndex}].outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(OuterCutOffRotation)));
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].location", KT.ActiveCamera.Location);
            KT.SetShaderVector(ShaderType.Lighting, $"spotLights[{_shaderIndex}].direction", KT.ActiveCamera.Front);
            KT.SetShaderColor(ShaderType.Lighting, $"spotLights[{_shaderIndex}].ambient", Color.Black);
            KT.SetShaderColor(ShaderType.Lighting, $"spotLights[{_shaderIndex}].diffuse", Color.White);
            KT.SetShaderColor(ShaderType.Lighting, $"spotLights[{ _shaderIndex}].specular", Color.White);
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
