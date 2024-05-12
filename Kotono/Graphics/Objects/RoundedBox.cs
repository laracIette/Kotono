using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;


namespace Kotono.Graphics.Objects
{
    internal class RoundedBox<T> : Object2D<T> where T : RoundedBoxSettings
    {
        public override Rect Rect
        {
            get => base.Rect;
            set
            {
                base.Rect = value;
                UpdateValues();
            }
        }

        protected float _fallOff;

        internal float FallOff
        {
            get => _fallOff;
            set
            {
                _fallOff = value;
                UpdateValues();
            }
        }

        protected float _cornerSize;

        internal float CornerSize
        {
            get => _cornerSize;
            set
            {
                _cornerSize = value;
                UpdateValues();
            }
        }

        protected virtual Shader Shader => ShaderManager.Shaders["roundedBox"];

        protected virtual Matrix4 Model =>
            Matrix4.CreateScale(
                (Rect + new Rect(w: FallOff * 2.0f)).NDC.Size.X,
                (Rect + new Rect(h: FallOff * 2.0f)).NDC.Size.Y,
                1.0f
            )
            * Matrix4.CreateTranslation(Rect.NDC.Position.X, Rect.NDC.Position.Y, 0.0f);

        internal RoundedBox(T settings)
            : base(settings)
        {
            Color = settings.Color;
            FallOff = settings.FallOff;
            CornerSize = settings.CornerSize;
        }

        protected virtual void UpdateValues()
        {
            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the box's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, 0.0f, Math.Min(Rect.Size.X, Rect.Size.Y) / 2.0f);

            /// FallOff has :
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl
            _fallOff = Math.Max(0.000001f, FallOff);
        }

        public override void Draw()
        {
            Shader.SetMatrix4("model", Model);
            Shader.SetColor("color", Color);
            Shader.SetRect("sides", Sides);
            Shader.SetFloat("fallOff", FallOff);
            Shader.SetFloat("cornerSize", CornerSize);

            SquareVertices.Draw();
        }

        protected Rect Sides
        {
            get
            {
                var r = Rect with 
                { 
                    Position = new Point(
                        WindowComponentManager.ActiveViewport.Rect.Position.X + Position.X,
                        Window.Rect.Size.Y - WindowComponentManager.ActiveViewport.Rect.Position.Y - Position.Y
                    ) 
                };

                return new Rect(
                    r.Position.X - r.Size.X / 2, // Left
                    r.Position.X + r.Size.X / 2, // Right
                    r.Position.Y + r.Size.Y / 2, // Top
                    r.Position.Y - r.Size.Y / 2  // Bottom
                );
            }
        }
    }
}
