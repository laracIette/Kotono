using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class Image : Object2D
    {
        private readonly ImageTexture _texture;

        public override Shader Shader => ShaderManager.Shaders["image"];

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        internal Image(string texturePath)
        {
            _texture = new ImageTexture(texturePath);
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Shader.SetMatrix4("model", Rect.Model);
            Shader.SetColor("color", Color);

            _texture.Draw();
        }

        public override string ToString() => _texture.Path;
    }
}
