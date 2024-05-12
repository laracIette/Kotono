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

        internal bool IsMouseOn => Rect.Overlaps(Rect, Mouse.Position);

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
            if (Time.Now < _endTime)
            {
                Rect += _transformation * Time.Delta;
            }
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            ShaderManager.Shaders["image"].SetMatrix4("model", Rect.Model);
            ShaderManager.Shaders["image"].SetColor("color", Color);

            _texture.Draw();
        }

        /// <summary>
        /// Transform the rect of the <see cref="Image"/> in a given time span.
        /// </summary>
        /// <param name="r"> The transformation to add. </param>
        /// <param name="duration"> The duration of the transformation. </param>
        internal void SetTransformation(Rect r, float duration)
        {
            if (duration <= 0.0f)
            {
                Rect = r;
            }
            else
            {
                _transformation = r / duration;

                _startTime = Time.Now;
                _endTime = _startTime + duration;
            }
        }

        public override string ToString() => Path;
    }
}
