using Kotono.Utils;
using System.Collections.Generic;

namespace Kotono.Graphics.Shaders
{
    internal static class ShaderManager
    {
        private static readonly List<Shader> _shaders = [];

        internal static void Create(Shader shader) => _shaders.TryAddDistinct(shader);

        internal static void Update() => _shaders.ForEach(s => s.Update());
    }
}
