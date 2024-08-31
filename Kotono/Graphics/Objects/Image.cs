using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class Image(string texturePath)
        : Object2D()
    {
        private readonly ImageTexture _texture = new(texturePath);

        public override Shader Shader => ImageShader.Instance;

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        static Image() => GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

        public override void UpdateShader()
        {
            if (Shader is ImageShader imageShader)
            {
                imageShader.SetModel(Rect.Model);
                imageShader.SetColor(Color);
            }
        }

        public override void Draw()
        {
            _texture.Draw();
        }

        public override string ToString() => _texture.Path;
    }
}
