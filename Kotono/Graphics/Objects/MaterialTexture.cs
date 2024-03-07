using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class MaterialTexture(MaterialTextureSettings settings)
        : Texture(settings.Path, TextureUnit.Texture0 + settings.TextureUnit)
    {
        internal string Name { get; set; } = settings.Name;
    }
}
