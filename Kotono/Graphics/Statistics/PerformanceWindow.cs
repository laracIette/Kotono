using Kotono.File;
using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Statistics
{
    internal static class PerformanceWindow
    {
        private static readonly RoundedBox _background;

        private static readonly RateStat _frame;

        private static readonly RateStat _update;

        internal static int MaxFrameRate { get; set; } = 60;

        internal static double FrameTime => _frame.Time;

        internal static double FrameRate => _frame.Rate;

        internal static double UpdateTime => _update.Time;

        internal static double UpdateRate => _update.Rate;

        public static bool IsDraw
        {
            get => _background.IsDraw;
            set
            {
                _background.IsDraw = value;
                _frame.IsDraw = value;
                _update.IsDraw = value;
            }
        }

        public static Point Position => KT.Size - new Point(200.0f, 60.0f);

        static PerformanceWindow()
        {
            _frame = new RateStat(Anchor.Bottom);
            _update = new RateStat(Anchor.Top);

            _background = new RoundedBox(
                new RoundedBoxSettings
                {
                    Dest = new Rect(Position, 400.0f, 120.0f),
                    Color = Color.DarkGray,
                    FallOff = 1.0f,
                    CornerSize = 10.0f
                }
            );
        }

        internal static void AddFrameTime(double frameTime)
        {
            _frame.AddTime(frameTime);
        }

        internal static void AddUpdateTime(double updateTime)
        {
            _update.AddTime(updateTime);
        }

        internal static void UpdatePosition()
        {
            _frame.Position = Position;
            _update.Position = Position;

            _background.Position = Position;
        }
    }
}
