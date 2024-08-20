using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class Image : Object2D
    {
        private static readonly Shader _shader = ShaderManager.Shaders["image"];

        private readonly Texture _texture;

        /// <summary>
        /// The path to the texture of the Image.
        /// </summary>
        internal string TexturePath { get; } = string.Empty;

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        internal Image(string texturePath)
        {
            TexturePath = texturePath;
            _texture = new Texture(TexturePath, TextureUnit.Texture0);
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _shader.SetMatrix4("model", Rect.Model);
            _shader.SetColor("color", Color);

            _texture.Draw();
        }

        public override string ToString() => TexturePath;
    }
}
