using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    internal class RoundedBorder : RoundedBox
    {
        private float _targetThickness;

        public float Thickness { get; protected set; }

        internal float TargetThickness
        {
            get => _targetThickness;
            set
            {
                _targetThickness = value;
                UpdateValues();
            }
        }

        public override Shader Shader => ShaderManager.Shaders["roundedBorder"];

        protected override Matrix4 Model => new NDCRect(RelativePosition, RelativeSize + new Point(FallOff * 2.0f + Thickness)).Model;

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

        public override void Draw()
        {
            Shader.SetFloat("thickness", Thickness);

            base.Draw();
        }
    }
}
