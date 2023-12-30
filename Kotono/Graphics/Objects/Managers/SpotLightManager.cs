using Kotono.Graphics.Objects.Lights;

namespace Kotono.Graphics.Objects.Managers
{
    internal class SpotLightManager()
        : DrawableManager<SpotLight>()
    {
        internal override void Create(SpotLight spotLight)
        {
            if (ObjectManager.GetSpotLights().Count >= SpotLight.MAX_COUNT)
            {
                KT.Print($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
            }
            else
            {
                base.Create(spotLight);
            }
        }
    }
}
