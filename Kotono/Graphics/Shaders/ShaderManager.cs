using System.Collections.Generic;

namespace Kotono.Graphics.Shaders
{
    internal static class ShaderManager
    {
        internal static Dictionary<string, Shader> Shaders { get; } = new()
        {
            ["hitbox"] = new Object3DShader("hitbox"),
            ["pointLight"] = new Object3DShader("pointLight"),
            ["image"] = new Shader("image"),
            ["gizmo"] = new Object3DShader("gizmo"),
            ["roundedBox"] = new Shader("roundedBox"),
            ["roundedBorder"] = new Shader("roundedBorder"),
            ["color"] = new TextureBufferShader("color"),
            ["blur"] = new TextureBufferShader("blur"),
            ["outline"] = new TextureBufferShader("outline"),
            ["flatTexture"] = new Object3DShader("flatTexture"),
            ["painter"] = new Shader("painter"),
            ["shape2D"] = new Shader("shape2D")
        };

        internal static void Update()
        {
            foreach (var shader in Shaders.Values)
            {
                shader.Update();
            }
        }
    }
}
