using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Statistics
{
    internal sealed class PerformanceWindow : Object2D
    {
        private readonly RoundedBox _background = new()
        {
            Color = Color.DarkSlateGray,
            TargetCornerSize = 10.0f,
            TargetFallOff = 1.0f,
        };

        private readonly RateStat _frame = new()
        {
            Anchor = Anchor.Bottom,
        };

        private readonly RateStat _update = new()
        {
            Anchor = Anchor.Top,
        };

        internal float MaxFrameRate { get; set; } = 60.0f;

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
            => _background.Parent = _frame.Parent = _update.Parent = this;

        internal void AddFrameTime(float frameTime)
            => _frame.AddTime(frameTime);

        internal void AddUpdateTime(float updateTime)
            => _update.AddTime(updateTime);
    }
}
