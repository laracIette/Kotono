using Kotono.Graphics.Objects;
using Kotono.Utils;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Statistics
{
    internal class PerformanceWindow
    {
        private readonly RoundedBox _background = new()
        {
            RelativeSize = new Point(400.0f, 120.0f),
            Color = Color.DarkSlateGray,
            TargetCornerSize = 10.0f
        };

        private readonly RateStat _frame = new() { Anchor = Anchor.Bottom };

        private readonly RateStat _update = new() { Anchor = Anchor.Top };

        internal float MaxFrameRate { get; set; } = 60.0f;

        internal float FrameTime => _frame.Time;

        internal float FrameRate => _frame.Rate;

        internal float UpdateTime => _update.Time;

        internal float UpdateRate => _update.Rate;

        internal bool IsDraw
        {
            get => _background.IsDraw;
            set
            {
                _background.IsDraw = value;
                _frame.IsDraw = value;
                _update.IsDraw = value;
            }
        }

        internal static Point Position => Window.Size - new Point(200.0f, 60.0f);

        internal PerformanceWindow()
        {
            _background.RelativePosition = Position;
        }

        internal void AddFrameTime(float frameTime)
        {
            _frame.AddTime(frameTime);
        }

        internal void AddUpdateTime(float updateTime)
        {
            _update.AddTime(updateTime);
        }

        internal void UpdatePosition()
        {
            _frame.RelativePosition = Position;
            _update.RelativePosition = Position;

            _background.RelativePosition = Position;
        }
    }
}
