using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using System.Linq;

namespace Kotono.Graphics.Performance
{
    public class RateStat(Rect dest, Anchor anchor)
    {
        public double Rate { get; private set; }

        public double Time { get; private set; }

        private readonly double[] _times = new double[60];

        private int _timeIndex = 0;

        public bool IsDraw
        {
            get => _text.IsDraw;
            set => _text.IsDraw = value;
        }

        private readonly Text _text = new("0", dest, anchor, Color.White, 1.0f, 1);

        public void Update()
        {

        }

        public void AddTime(double newTime)
        {
            _times[_timeIndex] = newTime;
            _timeIndex = (_timeIndex + 1) % _times.Length;

            Time = _times.Sum() / _times.Length;

            Rate = 1.0 / Time;

            _text.SetText(Rate.ToString("0.00"));
        }
    }
}
