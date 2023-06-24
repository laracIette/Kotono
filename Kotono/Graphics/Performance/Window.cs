using Kotono.Graphics.Objects;

namespace Kotono.Graphics.Performance
{
    internal class Window
    {
        private Image _background;

        private readonly RateStat _frame;

        private readonly RateStat _update;

        private readonly Rect _dest;

        internal double FrameTime => _frame.Time;

        internal double FrameRate => _frame.Rate;

        internal double UpdateTime => _update.Time;

        internal double UpdateRate => _update.Rate;

        internal Window()
        {
            _dest = new Rect(1130, 660, 50, 60);

            _frame = new RateStat(_dest - new Rect(y: _dest.H / 2));
            _update = new RateStat(_dest + new Rect(y: _dest.H / 2));
        }

        internal void Init()
        {
            _background = KT.CreateImage(KT.KotonoPath + "Assets/PerformanceWindow/background.png", new Rect(_dest.X, _dest.Y, 300, 120));
            
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
