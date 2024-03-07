using Kotono.Graphics.Objects;
using Kotono.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics
{
    internal class PBRMaterial : IMaterial
    {
        public List<MaterialTexture> Textures { get; } = [];

        internal MaterialTexture? Albedo => Textures.FirstOrNull(t => t.Name == "Albedo");

        internal MaterialTexture? Normal => Textures.FirstOrNull(t => t.Name == "Normal");

        internal MaterialTexture? Metalness => Textures.FirstOrNull(t => t.Name == "Metalness");

        internal MaterialTexture? Roughness => Textures.FirstOrNull(t => t.Name == "Roughness");

        internal MaterialTexture? AmbientOcclusion => Textures.FirstOrNull(t => t.Name == "AmbientOcclusion");

        internal MaterialTextureSettings[] MaterialTextureSettings { get; }

        internal PBRMaterial(MaterialTextureSettings[] materialTextureSettings)
        {
            MaterialTextureSettings = materialTextureSettings;

            Textures.AddRange(materialTextureSettings.Select(settings => new MaterialTexture(settings)));
        }

        public void Use()
        {
            Textures.ForEach(t => t.Use());
        }
    }
}
