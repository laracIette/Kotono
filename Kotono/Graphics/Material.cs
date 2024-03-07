using Kotono.Graphics.Objects;
using Kotono.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics
{
    internal class Material : IMaterial
    {
        public List<MaterialTexture> Textures { get; } = [];

        internal MaterialTexture? Albedo => Textures.FirstOrNull(t => t.Name == "Albedo");

        internal MaterialTexture? Normal => Textures.FirstOrNull(t => t.Name == "Normal");

        internal MaterialTexture? Metalness => Textures.FirstOrNull(t => t.Name == "Metalness");

        internal MaterialTexture? Roughness => Textures.FirstOrNull(t => t.Name == "Roughness");

        internal MaterialTexture? AmbientOcclusion => Textures.FirstOrNull(t => t.Name == "AmbientOcclusion");

        internal MaterialTextureSettings[] MaterialTexturesSettings { get; }

        internal Material(MaterialTextureSettings[] materialTexturesSettings)
        {
            MaterialTexturesSettings = materialTexturesSettings;

            Textures.AddRange(materialTexturesSettings.Select(s => new MaterialTexture(s)));
        }

        public void Use()
        {
            Textures.ForEach(t => t.Use());
        }
    }
}
