using Kotono.Graphics.Objects.Lights;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal class PointLightManager
    {
        internal const int MAX = 100;

        private readonly List<PointLight> _pointLights = new();

        internal PointLightManager() { }

        internal void Create(PointLight pointLight)
        {
            if (_pointLights.Count >= MAX)
            {
                KT.Print($"The number of PointLight is already at its max value: {MAX}.");
            }
            else
            {
                _pointLights.Add(pointLight);
            }
        }

        internal void Delete(PointLight pointLight)
        {
            if (_pointLights.Count <= 0)
            {
                KT.Print($"The number of PointLight is already at 0.");
            }
            else
            {
                _pointLights.Remove(pointLight);
            }
        }

        internal int GetCount()
        {
            return _pointLights.Count;
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
