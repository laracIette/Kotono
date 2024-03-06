using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class PBRMaterial : IMaterial
    {
        internal Texture? Albedo { get; set; } = null;

        internal Texture? Normal { get; set; } = null;

        internal Texture? Metalness { get; set; } = null;

        internal Texture? Roughness { get; set; } = null;

        internal Texture? AmbientOcclusion { get; set; } = null;

        internal string[] Paths
        {
            get
            {
                var result = new List<string>();

                if (Albedo != null) result.Add(Albedo.Path);
                if (Normal != null) result.Add(Normal.Path);
                if (Metalness != null) result.Add(Metalness.Path);
                if (Roughness != null) result.Add(Roughness.Path);
                if (AmbientOcclusion != null) result.Add(AmbientOcclusion.Path);

                return [.. result];
            }
        }

        public void Use()
        {
            Albedo?.Use();
            Normal?.Use();
            Metalness?.Use();
            Roughness?.Use();
            AmbientOcclusion?.Use();
        }
    }
}
