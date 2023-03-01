﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotono.Graphics.Objects
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

        public void Switch()
        {
            _isOn = !_isOn;
        }

        public void Update(float deltaTime)
        {
            if (_isOn)
            {
                if (OuterCutOffAngle >= 5.0f)
                {
                    CutOffAngle += 100.0f * deltaTime;
                }
                OuterCutOffAngle += 100.0f * deltaTime;
            }
            else
            {
                CutOffAngle -= 100.0f * deltaTime;
                OuterCutOffAngle -= 100.0f * deltaTime;
            }
        }

        public float CutOffAngle
        {
            get { return _cutOffAngle; }
            private set { _cutOffAngle = MathHelper.Clamp(value, 0.0f, 12.5f); }
        }

        public float OuterCutOffAngle
        {
            get { return _outerCutOffAngle; }
            private set { _outerCutOffAngle = MathHelper.Clamp(value, 0.0f, 17.5f); }
        }
    }
}
