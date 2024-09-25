using System.Collections.Generic;

namespace Kotono.Graphics.Shaders
{
    internal static class ShaderManager
    {
        private static readonly HashSet<Shader> _shaders = [];

        internal static void Create(Shader shader) => _shaders.Add(shader);

        internal static void Update()
        {
            foreach (var shader in _shaders)
            {
                shader.Update();
            }
        }
    }
}
