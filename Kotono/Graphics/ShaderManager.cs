using System;

namespace Kotono.Graphics
{
    public sealed class ShaderManager
    {
        private static readonly Lazy<Shader> _lighting = new(() => new("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag"));

        private static readonly Lazy<Shader> _pointLight = new(() => new("Graphics/Shaders/shader.vert", "Graphics/Shaders/pointLight.frag"));

        private static readonly Lazy<Shader> _hitbox = new(() => new("Graphics/Shaders/hitbox.vert", "Graphics/Shaders/hitbox.frag"));

        public static Shader Lighting => _lighting.Value;

        public static Shader PointLight => _pointLight.Value;

        public static Shader Hitbox => _hitbox.Value;

        private ShaderManager() { }
    }
}
