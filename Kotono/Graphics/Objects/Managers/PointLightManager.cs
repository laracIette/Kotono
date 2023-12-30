using Kotono.Graphics.Objects.Lights;

namespace Kotono.Graphics.Objects.Managers
{
    internal class PointLightManager()
        : DrawableManager<PointLight>()
    {
        internal override void Create(PointLight pointLight)
        {
            if (ObjectManager.GetPointLights().Count >= PointLight.MAX_COUNT)
            {
                KT.Print($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
            }
            else
            {
                base.Create(pointLight);
            }
        }
    }
}
