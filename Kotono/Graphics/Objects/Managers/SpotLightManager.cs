using Kotono.Graphics.Objects.Lights;

namespace Kotono.Graphics.Objects.Managers
{
    internal class SpotLightManager : DrawableManager<SpotLight>
    {
        internal SpotLightManager()
            : base() { }

        internal override void Create(SpotLight spotLight)
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

        internal override void Delete(SpotLight spotLight)
        {
            _drawables.ForEach(p => p.UpdateIndex());
            base.Delete(spotLight);
        }
    }
}
