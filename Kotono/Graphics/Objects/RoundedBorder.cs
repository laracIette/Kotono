using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal sealed class RoundedBorder : RoundedBox
    {
        private float _targetThickness;

        public float Thickness { get; private set; }

        internal float TargetThickness
        {
            get => _targetThickness;
            set
            {
                _targetThickness = value;
                UpdateValues();
            }
        }

        public override Shader Shader => RoundedBorderShader.Instance;

        protected override Matrix4 Model => new NDCRect(WorldPosition, FallOff * 2.0f + Thickness + WorldSize).Model;

        protected override void UpdateValues()
        {
            float minSize = Point.Min(Rect.RelativeSize);

            /// Thickness has :
            ///     a minimum value of 0,
            ///     a maximum value of the smallest value between the border's Width and Height
            Thickness = Math.Clamp(TargetThickness, 0.0f, minSize);

            /// FallOff has : 
            ///     a minimum value of 0,
            ///     a maximum value of the difference between the border's Width and its Thickness
            FallOff = Math.Clamp(TargetFallOff, 0.0f, Rect.RelativeSize.X - Thickness);

            /// CornerSize has :
            ///     a minimum value of half the border's Thickness + its FallOff,
            ///     a maximum value of half the smallest value between the border's Width and Height
            CornerSize = Math.Clamp(TargetCornerSize, Math.Half(Thickness) + FallOff, Math.Half(minSize));
        }

        public override void UpdateShader()
        {
            if (Shader is RoundedBorderShader roundedBorderShader)
            {
                roundedBorderShader.SetModel(Model);
                roundedBorderShader.SetColor(Color);
                roundedBorderShader.SetSides(Sides);
                roundedBorderShader.SetFallOff(FallOff);
                roundedBorderShader.SetCornerSize(CornerSize);
                roundedBorderShader.SetThickness(Thickness);
            }
        }
    }
}
