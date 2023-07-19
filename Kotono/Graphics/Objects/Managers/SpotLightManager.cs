using Kotono.Graphics.Objects.Lights;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    public class SpotLightManager : DrawableManager
    {
        private readonly List<SpotLight> _spotLights = new();

        public SpotLightManager()
            : base() { }

        public void Create(SpotLight spotLight)
        {
            if (SpotLight.Count >= SpotLight.MAX_COUNT)
            {
                KT.Print($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
            }
            else
            {
                _spotLights.Add(spotLight);
                base.Create(spotLight);
                SpotLight.Count++;
            }
        }

        public void Delete(SpotLight spotLight)
        {
            _spotLights.Remove(spotLight);
            _spotLights.ForEach(p => p.UpdateIndex());
            base.Delete(spotLight);
        }

        public int GetCount()
        {
            return _spotLights.Count;
        }
    }
}
