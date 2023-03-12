namespace Kotono.Graphics
{
    public static class ShaderManager
    {
        public static readonly Shader LightingShader = new("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag");
    }
}
