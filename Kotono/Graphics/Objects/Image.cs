﻿using Kotono.Graphics.Shaders;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal class Image : Object2D<ImageSettings>
    {
        private readonly Texture _texture;

        internal string Path { get; }

        private Rect _transformation;

        private float _startTime = 0;

        private float _endTime = 0;

        private Matrix4 Model =>
            Matrix4.CreateScale(Dest.NDC.W, Dest.NDC.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.NDC.X, Dest.NDC.Y, 0.0f);

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

            ShaderManager.Image.SetMatrix4("model", Model);
            ShaderManager.Image.SetColor("color", Color);

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
    }
}
