using Kotono.Graphics.Objects;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal interface IMaterial
    {
        public List<MaterialTexture> Textures { get; }

        public void Use();
    }
}
