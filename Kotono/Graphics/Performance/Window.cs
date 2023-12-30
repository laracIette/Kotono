using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Performance
{
    internal class Window
    {
        private readonly RoundedBox _background;

        private readonly RateStat _frame;

        private readonly RateStat _update;

        private readonly Rect _dest;

        internal double FrameTime => _frame.Time;

        internal double FrameRate => _frame.Rate;

        internal double UpdateTime => _update.Time;

        internal double UpdateRate => _update.Rate;

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

        internal Window()
        {
            _dest = new Rect(1080.0f, 660.0f, 50.0f, 60.0f);

            _frame = new RateStat(_dest, Anchor.Bottom);
            _update = new RateStat(_dest, Anchor.Top);

            _background = new RoundedBox(
                new Rect(_dest.X, _dest.Y, 400.0f, 120.0f),
                Color.FromHex("#273f45"),
                0,
                1.0f,
                10.0f
            );
        }

        internal void Init()
        {
            _frame.Init();
            _update.Init();
        }

        internal void Update()
        {
            _frame.Update();
            _update.Update();
        }

        internal void AddFrameTime(double frameTime)
        {
            _frame.AddTime(frameTime);
        }

        internal void AddUpdateTime(double updateTime)
        {
            _update.AddTime(updateTime);
        }
    }
}
