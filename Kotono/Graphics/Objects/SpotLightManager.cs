using Kotono.Graphics.Objects.Lights;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class SpotLightManager
    {
        internal const int MAX = 1;

        private readonly List<SpotLight> _spotLights = new();

        internal SpotLightManager() { }

        internal void Create(SpotLight spotLight)
        {
            if (_spotLights.Count >= MAX)
            {
                KT.Print($"The number of SpotLight is already at its max value: {MAX}.");
            }
            else
            {
                _spotLights.Add(spotLight);
            }
        }

        internal void Delete(SpotLight spotLight)
        {
            if (_spotLights.Count <= 0)
            {
                KT.Print($"The number of SpotLight is already at 0.");
            }
            else
            {
                _spotLights.Remove(spotLight);
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
