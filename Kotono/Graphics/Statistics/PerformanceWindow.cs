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

        internal float MaxFrameRate { get; set; } = 60.0f;

        internal float FrameTime => _frame.Time;

        internal float FrameRate => _frame.Rate;

        internal float UpdateTime => _update.Time;

        internal float UpdateRate => _update.Rate;

        internal PerformanceWindow()
        {
            _frame = new RateStat
            {
                Anchor = Anchor.Bottom,
                Parent = this
            };

            _update = new RateStat
            {
                Anchor = Anchor.Top,
                Parent = this
            };

            _background = new RoundedBox
            {
                RelativeSize = new Point(400.0f, 120.0f),
                Color = Color.DarkSlateGray,
                TargetCornerSize = 10.0f,
                Parent = this
            };
        }

        public override void Update() => RelativePosition = Rect.GetPositionFromAnchor(Window.Size, _background.RelativeSize, Anchor.BottomRight);

        internal void AddFrameTime(float frameTime) => _frame.AddTime(frameTime);

        internal void AddUpdateTime(float updateTime) => _update.AddTime(updateTime);
    }
}
