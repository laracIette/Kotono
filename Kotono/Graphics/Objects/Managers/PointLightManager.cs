using Kotono.Graphics.Objects.Lights;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    public class PointLightManager : DrawableManager
    {
        private readonly List<PointLight> _pointLights = new();

        public PointLightManager()
            : base() { }

        public void Create(PointLight pointLight)
        {
            if (PointLight.Count >= PointLight.MAX_COUNT)
            {
                KT.Print($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
            }
            else
            {
                _pointLights.Add(pointLight);
                base.Create(pointLight);
                PointLight.Count++;
            }
        }

        public void Delete(PointLight pointLight)
        {
            _pointLights.Remove(pointLight);
            _pointLights.ForEach(p => p.UpdateIndex());
            base.Delete(pointLight);
        }

        public PointLight GetFirst()
        {
            return _pointLights.First();
        }
    }
}
