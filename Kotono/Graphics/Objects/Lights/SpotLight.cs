using OpenTK.Windowing.GraphicsLibraryFramework;

using Kotono.Utils;
using System;
using Math = Kotono.Utils.Math;
using Kotono.Input;
using Kotono.Graphics.Objects.Managers;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight : IDrawable
    {
        public bool IsDraw { get; set; }

        private float _cutOffAngle = 12.5f;

        private float _outerCutOffAngle = 17.5f;

        private bool _isOn = true;

        private int _shaderIndex;

        private float CutOffAngle
        {
            get => _cutOffAngle;
            set => _cutOffAngle = Math.Clamp(value, 0.0f, 12.5f);
        }

        private float OuterCutOffAngle
        {
            get => _outerCutOffAngle;
            set => _outerCutOffAngle = Math.Clamp(value, 0.0f, 17.5f);
        }

        public const int MAX_COUNT = 1;

        public static int Count { get; internal set; }

        public SpotLight()
        {
            _shaderIndex = Count;
        }

        public void Init() { }

        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.F))
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
            ShaderManager.Lighting.SetFloat($"spotLights[{_shaderIndex}].cutOff", Math.Cos(Math.Rad(CutOffAngle)));
            ShaderManager.Lighting.SetFloat($"spotLights[{_shaderIndex}].outerCutOff", Math.Cos(Math.Rad(OuterCutOffAngle)));
            ShaderManager.Lighting.SetVector($"spotLights[{_shaderIndex}].location", CameraManager.ActiveCamera.Location);
            ShaderManager.Lighting.SetVector($"spotLights[{_shaderIndex}].direction", CameraManager.ActiveCamera.Front);
            ShaderManager.Lighting.SetColor($"spotLights[{_shaderIndex}].ambient", Color.Black);
            ShaderManager.Lighting.SetColor($"spotLights[{_shaderIndex}].diffuse", Color.White);
            ShaderManager.Lighting.SetColor($"spotLights[{ _shaderIndex}].specular", Color.White);
            ShaderManager.Lighting.SetFloat($"spotLights[{_shaderIndex}].constant", 1.0f);
            ShaderManager.Lighting.SetFloat($"spotLights[{_shaderIndex}].linear", 0.09f);
            ShaderManager.Lighting.SetFloat($"spotLights[{_shaderIndex}].quadratic", 0.032f);
        }

        public void UpdateIndex()
        {
            _shaderIndex--;
        }

        public void Draw()
        {

        }

        public void Save()
        {

        }

        public void Dispose()
        {
            Count--;
            GC.SuppressFinalize(this);
        }
    }
}
