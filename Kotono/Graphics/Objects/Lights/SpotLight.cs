using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight() : 
        Object3D()
    {
        private float _cutOffAngle = 12.5f;

        private float _outerCutOffAngle = 17.5f;

        private bool _isOn = true;

        public float CutOffAngle
        {
            get => _cutOffAngle;
            set => _cutOffAngle = Math.Clamp(value, 0.0f, 12.5f);
        }

        public float OuterCutOffAngle
        {
            get => _outerCutOffAngle;
            set => _outerCutOffAngle = Math.Clamp(value, 0.0f, 17.5f);
        }

        public const int MAX_COUNT = 1;

        public override void Update()
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
    }
}
