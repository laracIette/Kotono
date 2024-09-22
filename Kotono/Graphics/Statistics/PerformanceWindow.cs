using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Statistics
{
    internal sealed class PerformanceWindow : Object2D
    {
        private readonly RoundedBox _background;

        private readonly RateStat _frame;

        private readonly RateStat _update;

        internal float FrameTime => _frame.Time;

        internal float FrameRate => _frame.Rate;

        internal float UpdateTime => _update.Time;

        internal float UpdateRate => _update.Rate;

        public override Point RelativeSize
        {
            get => base.RelativeSize;
            set => base.RelativeSize = _background.RelativeSize = value;
        }

        internal PerformanceWindow()
        {
            _background = new RoundedBox
            {
                Color = Color.DarkSlateGray,
                TargetCornerSize = 10.0f,
                TargetFallOff = 1.0f,
                Parent = this,
                RelativePosition = Point.Zero,
                Layer = Layer,
            };

            _frame = new RateStat
            {
                Anchor = Anchor.Bottom,
                Parent = this,
                RelativePosition = Point.Zero,
                Layer = Layer + 1,
            };

            _update = new RateStat
            {
                Anchor = Anchor.Top,
                Parent = this,
                RelativePosition = Point.Zero,
                Layer = Layer + 1,
            };
        }

        internal void AddFrameTime(float frameTime)
            => _frame.AddTime(frameTime);

        internal void AddUpdateTime(float updateTime)
            => _update.AddTime(updateTime);
    }
}
