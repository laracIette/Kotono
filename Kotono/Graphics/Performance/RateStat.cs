using Kotono.Graphics.Objects;
using Kotono.Utils;
using System.Linq;

namespace Kotono.Graphics.Performance
{
    public class RateStat
    {
        public double Rate { get; private set; }

        public double Time { get; private set; }

        private readonly double[] _times;

        private int _timeIndex;

        private readonly Text _text;

        public RateStat(Rect dest)
        {
            _times = new double[60];
            _timeIndex = 0;

            _text = new Text("0", dest, Anchor.Center, Color.White, 1f, 1);
        }

        public void Init()
        {
            _text.Init();
        }

        public void Update() 
        {

        }

        public void AddTime(double newTime)
        {
            _times[_timeIndex] = newTime;
            _timeIndex = (_timeIndex + 1) % _times.Length;

            Time = _times.Sum() / _times.Length;

            Rate = 1 / Time;

            _text.SetText(Rate.ToString("0.00"));
        }

        public void Show()
        {
            _text.Show();
        }

        public void Hide()
        {
            _text.Hide();
        }

    }
}
