using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;


namespace Kotono.Graphics.Objects
{
    internal class RoundedBox<T> : Object2D<T> where T : RoundedBoxSettings
    {
        public override Rect Dest
        {
            get => base.Dest;
            set
            {
                base.Dest = value;
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

        protected virtual Shader Shader => ShaderManager.RoundedBox;

        protected virtual Matrix4 Model =>
            Matrix4.CreateScale(
                (Dest + new Rect(w: FallOff * 2.0f)).NDC.W,
                (Dest + new Rect(h: FallOff * 2.0f)).NDC.H,
                1.0f
            )
            * Matrix4.CreateTranslation(Dest.NDC.X, Dest.NDC.Y, 0.0f);

        internal RoundedBox(T settings)
            : base(settings)
        {
            Color = settings.Color;
            FallOff = settings.FallOff;
            CornerSize = settings.CornerSize;
        }

        internal RoundedBox() : base() { }

        protected virtual void UpdateValues()
        {
            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the box's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, 0.0f, Math.Min(Dest.W, Dest.H) / 2.0f);

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
                var r = Dest;

                r.X = WindowComponentManager.ActiveViewport.Dest.X + r.X;
                r.Y = Window.Dest.H - WindowComponentManager.ActiveViewport.Dest.Y - r.Y;

                return new Rect(
                    r.X - r.W / 2, // Left
                    r.X + r.W / 2, // Right
                    r.Y + r.H / 2, // Top
                    r.Y - r.H / 2  // Bottom
                );
            }
        }
    }
}
