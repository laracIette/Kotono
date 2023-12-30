using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects.Lights
{
    public class SpotLight : IDrawable
    {
        public bool IsDraw { get; private set; }

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

        public SpotLight()
        {
            ObjectManager.Create(this);
        }

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
            GC.SuppressFinalize(this);
        }
    }
}
