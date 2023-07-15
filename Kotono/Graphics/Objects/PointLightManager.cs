using Kotono.Graphics.Objects.Lights;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal class PointLightManager
    {
        private readonly List<PointLight> _pointLights = new();

        internal PointLightManager() { }

        internal void Create(PointLight pointLight)
        {
            if (PointLight.Count >= PointLight.MAX_COUNT)
            {
                KT.Print($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
            }
            else
            {
                _pointLights.Add(pointLight);
                PointLight.Count++;
            }
        }

        internal void Delete(PointLight pointLight)
        {
            pointLight.Dispose();
            _pointLights.Remove(pointLight);
        }

        internal PointLight GetFirst()
        {
            return _pointLights.First();
        }

        internal void Init()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.Init();
            }
        }

        internal void Update()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.Update();
            }
        }

        internal void UpdateShaders()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.UpdateShaders();
            }
        }

        internal void Draw()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.Draw();
            }
        }
    }
}
