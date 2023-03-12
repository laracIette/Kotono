using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Kotono.Utils;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight
    {
        private float _cutOffAngle = 12.5f;

        private float _outerCutOffAngle = 17.5f;

        private bool _isOn;

        public SpotLight(bool isOn)
        {
            _isOn = isOn;
        }

        public void Update()
        {
            if (InputManager.KeyboardState.IsKeyDown(Keys.F) && !InputManager.KeyboardState.WasKeyDown(Keys.F))
            {
                _isOn = !_isOn;
            }

            if (_isOn)
            {
                if (OuterCutOffAngle >= 5.0f)
                {
                    CutOffAngle += 100.0f * Time.Delta;
                }
                OuterCutOffAngle += 100.0f * Time.Delta;
            }
            else
            {
                CutOffAngle -= 100.0f * Time.Delta;
                OuterCutOffAngle -= 100.0f * Time.Delta;
            }
        }

        public float CutOffAngle
        {
            get => _cutOffAngle;
            private set => _cutOffAngle = MathHelper.Clamp(value, 0.0f, 12.5f);
        }

        public float OuterCutOffAngle
        {
            get => _outerCutOffAngle;
            private set => _outerCutOffAngle = MathHelper.Clamp(value, 0.0f, 17.5f);
        }
    }
}
