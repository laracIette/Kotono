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

        private Rect _transformation;

        private float _startTime = 0;

        private float _endTime = 0;

        internal bool IsMouseOn => Rect.Overlaps(Dest, Mouse.Position);

        internal Image(ImageSettings settings)
            : base(settings)
        {
            Path = settings.Texture;
            Color = settings.Color;

            _transformation = Rect.Zero;

            _texture = new Texture(Path);
        }

        public override void Update()
        {
            if (Time.Now >= _endTime)
            {
                _transformation = Rect.Zero;
            }

            Dest += _transformation * Time.Delta;
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            ShaderManager.Shaders["image"].SetMatrix4("model", Dest.Model);
            ShaderManager.Shaders["image"].SetColor("color", Color);

            _texture.Draw();
        }

        internal void Transform(Rect transformation)
        {
            Dest += transformation;
        }

        internal void Transform(Rect transformation, float time)
        {
            _transformation += transformation / time;

            _startTime = Time.Now;
            _endTime = _startTime + time;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
