﻿namespace Kotono.Graphics
{
    public static class ShaderManager
    {
        public static readonly Shader LightingShader = new Shader("Graphics/Shaders/shader.vert", "Graphics/Shaders/lighting.frag");
        public static readonly Shader LampShader = new Shader("Graphics/Shaders/shader.vert", "Graphics/Shaders/shader.frag");
    }
}