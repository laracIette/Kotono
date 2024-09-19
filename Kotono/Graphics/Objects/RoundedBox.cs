using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;


namespace Kotono.Graphics.Objects
{
    internal class RoundedBox : Object2D
    {
        protected float _targetFallOff;

        protected float _targetCornerSize;

        public float FallOff { get; protected set; }

        public float CornerSize { get; protected set; }

        internal float TargetFallOff
        {
            get => _targetFallOff;
            set
            {
                _targetFallOff = value;
                UpdateValues();
            }
        }

        internal float TargetCornerSize
        {
            get => _targetCornerSize;
            set
            {
                _targetCornerSize = value;
                UpdateValues();
            }
        }

        public override Point RelativeSize
        {
            get => base.RelativeSize;
            set
            {
                base.RelativeSize = value;
                UpdateValues();
            }
        }

        public override Shader Shader => RoundedBoxShader.Instance;

        protected virtual Matrix4 Model => new NDCRect(WorldPosition, FallOff * 2.0f + WorldSize).Model;

        protected virtual void UpdateValues()
        {
            float minSize = Point.Min(Rect.RelativeSize);

            /// CornerSize has : 
            ///     a minimum value of 0,
            ///     a maximum value of half the smallest value between the box's Width and Height
            CornerSize = Math.Clamp(TargetCornerSize, 0.0f, Math.Half(minSize));

            /// FallOff has :
            ///     a minimum value of 0
            FallOff = Math.Max(0.0f, TargetFallOff);
        }

        public override void UpdateShader()
        {
            if (Shader is RoundedBoxShader roundedBoxShader)
            {
                roundedBoxShader.SetModel(Model);
                roundedBoxShader.SetColor(Color);
                roundedBoxShader.SetSides(Sides);
                roundedBoxShader.SetFallOff(FallOff);
                roundedBoxShader.SetCornerSize(CornerSize);
            }
        }

        public override void Draw()
        {
            SquareVertices.Draw();
        }

        protected Sides Sides
        {
            get
            {
                var position = new Point(
                    Viewport.Active.RelativePosition.X + RelativePosition.X,
                    Window.Size.Y - Viewport.Active.RelativePosition.Y - RelativePosition.Y
                );

                return new Sides(
                    position.X - Math.Half(RelativeSize.X),
                    position.X + Math.Half(RelativeSize.X),
                    position.Y + Math.Half(RelativeSize.Y),
                    position.Y - Math.Half(RelativeSize.Y)
                );
            }
        }
    }
}
