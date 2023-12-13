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

        internal Window()
        {
            _dest = new Rect(1080, 660, 50, 60);

            _frame = new RateStat(_dest, Anchor.Bottom);
            _update = new RateStat(_dest, Anchor.Top);

            _background = new RoundedBox(
                new Rect(_dest.X, _dest.Y, 400, 120),
                Color.FromHex("#273f45"),
                layer: 0,
                fallOff: 1,
                cornerSize: 10
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

        internal void Show()
        {
            _background.Show();
            _frame.Show();
            _update.Show();
        }

        internal void Hide()
        {
            _background.Hide();
            _frame.Hide();
            _update.Hide();
        }
    }
}
