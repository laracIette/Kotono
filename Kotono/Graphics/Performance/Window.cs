using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Graphics.Performance
{
    public class Window
    {
        private readonly RoundedBox _background;

        private readonly RateStat _frame;

        private readonly RateStat _update;

        private readonly Rect _dest;

        public double FrameTime => _frame.Time;

        public double FrameRate => _frame.Rate;

        public double UpdateTime => _update.Time;

        public double UpdateRate => _update.Rate;

        public Window()
        {
            _dest = new Rect(1080, 660, 50, 60);

            _frame = new RateStat(_dest - new Rect(y: _dest.H / 2));
            _update = new RateStat(_dest + new Rect(y: _dest.H / 2));
            
            _background = new RoundedBox(
                new Rect(_dest.X, _dest.Y, 400, 120), 
                Color.FromHex("#273f45"), 
                layer: 0, 
                fallOff: 1,  
                cornerSize: 10
            );
        }

        public void Init()
        {
            _frame.Init();
            _update.Init();
        }

        public void Update()
        {
            _frame.Update();
            _update.Update();
        }

        public void AddFrameTime(double frameTime)
        {
            _frame.AddTime(frameTime);
        }

        public void AddUpdateTime(double updateTime)
        {
            _update.AddTime(updateTime);
        }

        public void Show()
        {
            _background.Show();
            _frame.Show();
            _update.Show();
        }

        public void Hide()
        {
            _background.Hide();
            _frame.Hide();
            _update.Hide();
        }
    }
}
