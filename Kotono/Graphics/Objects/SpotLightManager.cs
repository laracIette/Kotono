using Kotono.Graphics.Objects.Lights;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class SpotLightManager
    {

        private readonly List<SpotLight> _spotLights = new();

        internal SpotLightManager() { }

        internal void Create(SpotLight spotLight)
        {
            if (SpotLight.Count >= SpotLight.MAX_COUNT)
            {
                KT.Print($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
            }
            else
            {
                _spotLights.Add(spotLight);
                SpotLight.Count++;
            }
        }

        internal void Delete(SpotLight spotLight)
        {
            spotLight.Dispose();
            _spotLights.Remove(spotLight);
        }

        internal void Init()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.Init();
            }
        }

        internal void Update()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.Update();
            }
        }

        internal void UpdateShaders()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.UpdateShaders();
            }
        }

        internal int GetCount()
        {
            return _spotLights.Count;
        }

        internal void Draw()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.Draw();
            }
        }
    }
}
