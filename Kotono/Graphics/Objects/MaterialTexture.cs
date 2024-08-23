using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class MaterialTexture(string path, TextureUnit textureUnit)
        : Texture(path, textureUnit)
    {
        internal float Strength { get; set; } = 1.0f;
    }
}
