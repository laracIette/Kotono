using Kotono.Graphics.Objects.Lights;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal class PointLightManager
    {
        public const int MAX = 100;

        private readonly List<PointLight> _pointLights = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _pointLightIndex = 0;

        public PointLightManager() { }

        public int Create(PointLight pointLight)
        {
            if (_pointLights.Count >= MAX)
            {
                throw new Exception($"The number of PointLight is already at its max value: {MAX}.");
            }

            _indexOffset[_pointLightIndex] = _pointLights.Count;

            _pointLights.Add(pointLight);

            return _pointLightIndex++;
        }

        public void Delete(int index)
        {
            if (_pointLights.Count <= 0)
            {
                throw new Exception($"The number of PointLight is already at 0.");
            }

            _pointLights[_indexOffset[index]].Dispose();
            _pointLights.RemoveAt(_indexOffset[index]);
            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                    _pointLights[_indexOffset[i]].UpdateIndex();
                }
            }
        }

        public int GetCount()
            => _pointLights.Count;

        public int GetFirstIndex()
            => _indexOffset.First().Key;

        public void Update()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.Update();
            }
        }

        public void UpdateShaders()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.UpdateShaders();
            }
        }

        public void Draw()
        {
            foreach (var pointLight in _pointLights)
            {
                pointLight.Draw();
            }
        }
    }
}
