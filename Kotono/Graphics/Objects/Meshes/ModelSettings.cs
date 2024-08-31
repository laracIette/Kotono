﻿using Kotono.Graphics.Shaders;

namespace Kotono.Graphics.Objects.Meshes
{
    internal class ModelSettings
    {
        internal string Path { get; set; } = string.Empty;

        internal Shader Shader { get; set; } = LightingShader.Instance;
    }
}
