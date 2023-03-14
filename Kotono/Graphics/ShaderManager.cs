namespace Kotono.Graphics
{
    public static class ShaderManager
    {
        public static readonly Shader Lighting = new("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag");
        public static readonly Shader PointLight = new("Graphics/Shaders/shader.vert", "Graphics/Shaders/pointLight.frag");
    }
}
