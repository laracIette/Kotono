using Kotono.Graphics.Objects;

namespace Kotono.Graphics
{
    internal class Material : IMaterial
    {
        internal MaterialTexture? Albedo { get; set; }

        internal MaterialTexture? Normal { get; set; }

        internal MaterialTexture? Metallic { get; set; }

        internal MaterialTexture? Roughness { get; set; }

        internal MaterialTexture? AmbientOcclusion { get; set; }

        internal MaterialTexture? Emissive { get; set; }

        public void Use()
        {
            Albedo?.Use();
            Normal?.Use();
            Metallic?.Use();
            Roughness?.Use();
            AmbientOcclusion?.Use();
            Emissive?.Use();
        }
    }
}
