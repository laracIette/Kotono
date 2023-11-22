using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects
{
    public class Image : IObject2D
    {
        private readonly int _texture;

        public string Path { get; }

        private Rect _dest;

        public Rect Dest 
        {
            get => _dest;
            set => _dest = value;
        }

        public Point Position
        {
            get => _dest.Position;
            set => _dest.Position = value;
        }

        public Point Size
        {
            get => _dest.Size;
            set => _dest.Size = value;
        }

        public float X 
        {
            get => _dest.X;
            set => _dest.X = value;
        }

        public float Y
        {
            get => _dest.Y;
            set => _dest.Y = value;
        }

        public float W 
        {
            get => _dest.W;
            set => _dest.W = value;
        }

        public float H
        {
            get => _dest.H;
            set => _dest.H = value;
        }

        public Color Color { get; set; }

        private Rect _transformation;

        private double _startTime = 0;

        private double _endTime = 0;

        private Matrix4 Model =>
            Matrix4.CreateScale(Dest.WorldSpace.W, Dest.WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public bool IsDraw { get; private set; } = true;

        public int Layer { get; set; } = 0;

        public Image(ImageSettings settings)
        {
            Path = settings.Path;
            Dest = settings.Dest;
            Color = settings.Color;
            Layer = settings.Layer;

            _transformation = new Rect();

            _texture = TextureManager.LoadTexture(Path);

            ObjectManager.Create(this);
        }

        public void Init() { }

        public virtual void Update()
        {
            if (Time.NowS >= _endTime)
            {
                _transformation = Rect.Zero;
            }
            
            Dest += _transformation * Time.DeltaS;
        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            TextureManager.UseTexture(_texture, TextureUnit.Texture0);

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

        public void TransformTo(Rect dest)
        {
            Transform(dest - Dest);
        }

        public void TransformTo(Rect dest, double time)
        {
            Transform(dest - Dest, time);
        }

        public bool IsMouseOn()
        {
            return Rect.Overlaps(Dest, Mouse.RelativePosition);
        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void Save()
        {

        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
