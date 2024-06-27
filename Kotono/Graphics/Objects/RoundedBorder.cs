using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal class RoundedBorder : RoundedBox<RoundedBorderSettings>
    {
        internal float TargetThickness { get; private set; }

        private float _thickness;

        internal float Thickness
        {
            get => _thickness;
            set
            {
                TargetThickness = value;
                UpdateValues();
            }
        }

        protected override Shader Shader => ShaderManager.Shaders["roundedBorder"];

        protected override Matrix4 Model => new NDCRect(Position, Size + new Point(FallOff * 2.0f + Thickness)).Model;

        internal RoundedBorder(RoundedBorderSettings settings)
            : base(settings)
        {
            Thickness = settings.Thickness;
        }

        protected override void UpdateValues()
        {
            float minSize = Point.Min(Rect.BaseSize);

            /// Thickness has :
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the border's Width and Height
            _thickness = Math.Clamp(TargetThickness, 0.0f, minSize);

            /// CornerSize has :
            ///     a minimum value of the border's Thickness / 2 + its FallOff,
            ///     a maximum value of the smallest value between the border's Width and Height divided by 2
            _cornerSize = Math.Clamp(TargetCornerSize, Thickness / 2.0f + FallOff, minSize / 2.0f);

            /// FallOff has : 
            ///     a minimum value of 0.000001 so that there is no division by 0 in glsl,
            ///     a maximum value of the border's Width - its Thickness
            _fallOff = Math.Clamp(TargetFallOff, 0.000001f, Rect.BaseSize.X - Thickness);
        }

        public override void Draw()
        {
            Shader.SetFloat("thickness", Thickness);

            base.Draw();
        }
    }
}
