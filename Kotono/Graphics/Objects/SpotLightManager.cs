using Kotono.Graphics.Objects.Lights;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    public class SpotLightManager
    {
        public const int MAX = 1;

        private readonly List<SpotLight> _spotLights = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _spotLightIndex = 0;

        public SpotLightManager() { }

        public int Create(SpotLight spotLight)
        {
            if (_spotLights.Count >= MAX)
            {
                throw new Exception($"The number of SpotLight is already at its max value: {MAX}.");
            }

            _indexOffset[_spotLightIndex] = _spotLights.Count;

            _spotLights.Add(spotLight);

            return _spotLightIndex++;
        }

        public void Delete(int index)
        {
            if (_spotLights.Count <= 0)
            {
                throw new Exception($"The number of SpotLight is already at 0.");
            }

            _spotLights[_indexOffset[index]].Dispose();
            _spotLights.RemoveAt(_indexOffset[index]);
            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                    _spotLights[_indexOffset[i]].UpdateIndex();
                }
            }
        }

        public void Update()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.Update();
            }
        }

        public void UpdateShaders()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.UpdateShaders();
            }
        }

        public int GetCount()
            => _spotLights.Count;

        public void Draw()
        {
            foreach (var pointLight in _spotLights)
            {
                pointLight.Draw();
            }
        }
    }
}
