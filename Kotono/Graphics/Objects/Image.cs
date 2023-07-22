using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class Image : IDrawable
    {
        private readonly int _texture;

        public Rect Dest;

        public Color Color;

        private Rect _transformation;

        private double _startTime = 0;

        private double _endTime = 0;

        private Matrix4 Model =>
            Matrix4.CreateScale(Dest.WorldSpace.W, Dest.WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public bool IsDraw { get; private set; } = true;

        public Image(string path, Rect dest, Color color)
        {
            Dest = dest;

            Color = color;

            _transformation = new Rect();

            _texture = TextureManager.LoadTexture(path);

            ObjectManager.CreateImage(this);
        }

        public void Init() { }

        public virtual void Update()
        {
            if (Time.NowS < _endTime)
            {
                Dest += _transformation * Time.DeltaS;
            }

            // check if Image is out of screen bounds
            if (((Dest.X + Dest.W) < 0) || (Dest.X > KT.Dest.W) || ((Dest.Y + Dest.H) < 0) || (Dest.Y > KT.Dest.H))
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            TextureManager.UseTexture(_texture, TextureUnit.Texture0);

            ShaderManager.SetMatrix4(ShaderType.Image, "model", Model);
            ShaderManager.SetColor(ShaderType.Image, "color", Color);

            GL.BindVertexArray(SquareVertices.VertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Transform(Rect transformation, double time)
        {
            _transformation = transformation / (float)time;

            _startTime = Time.NowS;
            _endTime = _startTime + time;
        }

        public void TransformTo(Rect dest, double time)
        {
            Transform(dest - Dest, time);
        }

        public bool IsMouseOn()
        {
            return (Math.Abs(Mouse.RelativePosition.X - Dest.X) < Dest.W / 2) && (Math.Abs(Mouse.RelativePosition.Y - Dest.Y) < Dest.H / 2);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
