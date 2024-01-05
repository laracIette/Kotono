using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class Image : Object2D
    {
        private readonly Texture _texture;

        public string Path { get; }

        public Color Color { get; set; }

        private Rect _transformation;

        private double _startTime = 0;

        private double _endTime = 0;

        private Matrix4 Model =>
            Matrix4.CreateScale(Dest.NDC.W, Dest.NDC.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.NDC.X, Dest.NDC.Y, 0.0f);
        
        public bool IsMouseOn => Rect.Overlaps(Dest, Mouse.Position);

        public Image(ImageSettings settings)
            : base()
        {
            Path = settings.Path;
            Dest = settings.Dest;
            Color = settings.Color;
            Layer = settings.Layer;

            _transformation = Rect.Zero;

            _texture = new Texture(Path, TextureUnit.Texture0);
        }

        public override void Update()
        {
            if (Time.NowS >= _endTime)
            {
                _transformation = Rect.Zero;
            }

            Dest += _transformation * Time.DeltaS;
        }

        public override void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _texture.Use();

            ShaderManager.Image.SetMatrix4("model", Model);
            ShaderManager.Image.SetColor("color", Color);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Transform(Rect transformation)
        {
            Dest += transformation;
        }

        public void Transform(Rect transformation, double time)
        {
            _transformation += transformation / (float)time;

            _startTime = Time.NowS;
            _endTime = _startTime + time;
        }
    }
}
