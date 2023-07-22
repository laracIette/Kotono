using Kotono.Graphics.Objects.Lights;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    public class PointLightManager : DrawableManager<PointLight>
    {
        public PointLightManager()
            : base() { }

        public override void Create(PointLight pointLight)
        {
            if (PointLight.Count >= PointLight.MAX_COUNT)
            {
                KT.Print($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
            }
            else
            {
                base.Create(pointLight);
                PointLight.Count++;
            }
        }

        public override void Delete(PointLight pointLight)
        {
            _drawables.ForEach(p => p.UpdateIndex());
            base.Delete(pointLight);
        }

        public PointLight GetFirst()
        {
            return _drawables.First();
        }
    }
}
