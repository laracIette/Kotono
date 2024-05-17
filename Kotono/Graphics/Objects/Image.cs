using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    internal class Image : Object2D<ImageSettings>
    {
        private readonly Texture _texture;

        internal string Path { get; }

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

        internal Image(ImageSettings settings)
            : base(settings)
        {
            Path = settings.Texture;
            Color = settings.Color;

            _texture = new Texture(Path);
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            ShaderManager.Shaders["image"].SetMatrix4("model", Rect.Model);
            ShaderManager.Shaders["image"].SetColor("color", Color);

            _texture.Draw();
        }

        public override string ToString() => Path;
    }
}
