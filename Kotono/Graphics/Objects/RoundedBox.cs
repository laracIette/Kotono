﻿using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;


namespace Kotono.Graphics.Objects
{
    internal class RoundedBox : Object2D
    {
        public override Point Size 
        { 
            get => base.Size;
            set
            {
                base.Size = value;
                UpdateValues();
            }
        }

        internal float TargetFallOff { get; private set; }

        protected float _fallOff;

        internal float FallOff
        {
            get => _fallOff;
            set
            {
                TargetFallOff = value;
                UpdateValues();
            }
        }

        internal float TargetCornerSize { get; private set; }

        protected float _cornerSize;

        internal float CornerSize
        {
            get => _cornerSize;
            set
            {
                TargetCornerSize = value;
                UpdateValues();
            }
        }

        public override Shader Shader => ShaderManager.Shaders["roundedBox"];

        protected virtual Matrix4 Model => new NDCRect(Position, Size + new Point(FallOff * 2.0f)).Model;

        protected virtual void UpdateValues()
        {
            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the box's Width and Height divided by 2
            _cornerSize = Math.Clamp(TargetCornerSize, 0.0f, Math.Min(Rect.Size.X, Rect.Size.Y) / 2.0f);

            /// FallOff has :
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl
            _fallOff = Math.Max(0.000001f, TargetFallOff);
        }

        public override void Draw()
        {
            Shader.SetMatrix4("model", Model);
            Shader.SetColor("color", Color);
            Shader.SetSides("sides", Sides);
            Shader.SetFloat("fallOff", FallOff);
            Shader.SetFloat("cornerSize", CornerSize);

            SquareVertices.Draw();
        }

        private Sides Sides
        {
            get
            {
                var position = new Point(
                    Viewport.Active.Position.X + Position.X,
                    Window.Size.Y - Viewport.Active.Position.Y - Position.Y
                );

                return new Sides(
                    position.X - Size.X / 2,
                    position.X + Size.X / 2,
                    position.Y + Size.Y / 2,
                    position.Y - Size.Y / 2
                );
            }
        }
    }
}
