using Kotono.Graphics.Objects.Lights;

namespace Kotono.Graphics.Objects.Managers
{
    internal class SpotLightManager()
        : DrawableManager<SpotLight>()
    {
        internal override void Create(SpotLight spotLight)
        {
            if (Drawables.Count >= SpotLight.MAX_COUNT)
            {
                KT.Log($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
            }
            else
            {
                base.Create(spotLight);
            }
        }
    }
}
