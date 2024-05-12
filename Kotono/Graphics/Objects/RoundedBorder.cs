using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal class RoundedBorder : RoundedBox<RoundedBorderSettings>
    {
        private float _thickness;

        internal float Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                UpdateValues();
            }
        }

        protected override Shader Shader => ShaderManager.Shaders["roundedBorder"];

        protected override Matrix4 Model =>
            Matrix4.CreateScale(
                (Rect + new Rect(w: FallOff * 2.0f + Thickness)).NDC.Size.X,
                (Rect + new Rect(h: FallOff * 2.0f + Thickness)).NDC.Size.Y,
                1.0f
            )
            * Matrix4.CreateTranslation(Rect.NDC.Position.X, Rect.NDC.Position.Y, 0.0f);

        internal RoundedBorder(RoundedBorderSettings settings)
            : base(settings)
        {
            Thickness = settings.Thickness;
        }

        protected override void UpdateValues()
        {
            float minSize = Point.Min(Rect.Size);

            /// Thickness has :
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the border's Width and Height
            _thickness = Math.Clamp(Thickness, 0.0f, minSize);

            /// CornerSize has :
            ///     a minimum value of the border's Thickness / 2 + its FallOff,
            ///     a maximum value of the smallest value between the border's Width and Height divided by 2
            _cornerSize = Math.Clamp(CornerSize, Thickness / 2.0f + FallOff, minSize / 2.0f);

            /// FallOff has : 
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl,
            ///     a maximum value of the border's Width - its Thickness
            _fallOff = Math.Clamp(FallOff, 0.000001f, Rect.Size.X - Thickness);
        }

        public override void Draw()
        {
            Shader.SetFloat("thickness", Thickness);

            base.Draw();
        }
    }
}
