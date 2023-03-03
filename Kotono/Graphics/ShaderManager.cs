namespace Kotono.Graphics
{
    public static class ShaderManager
    {
        public static Shader LightingShader = new Shader("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag");
        public static Shader LampShader = new Shader("Graphics/Shaders/shader.vert", "Graphics/Shaders/shader.frag");
    }
}
