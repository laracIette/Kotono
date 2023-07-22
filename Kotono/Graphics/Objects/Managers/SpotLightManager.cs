using Kotono.Graphics.Objects.Lights;

namespace Kotono.Graphics.Objects.Managers
{
    public class SpotLightManager : DrawableManager<SpotLight>
    {
        public SpotLightManager()
            : base() { }

        public override void Create(SpotLight spotLight)
        {
            if (SpotLight.Count >= SpotLight.MAX_COUNT)
            {
                KT.Print($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
            }
            else
            {
                base.Create(spotLight);
                SpotLight.Count++;
            }
        }

        public override void Delete(SpotLight spotLight)
        {
            _drawables.ForEach(p => p.UpdateIndex());
            base.Delete(spotLight);
        }

        public int GetCount()
        {
            return _drawables.Count;
        }
    }
}
