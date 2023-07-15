using Kotono.Graphics.Objects;
using Kotono.Utils;
using System.Linq;

namespace Kotono.Graphics.Performance
{
    internal class RateStat
    {
        internal double Rate { get; private set; }

        internal double Time { get; private set; }

        private readonly double[] _times;

        private int _timeIndex;

        private readonly Text _text;

        internal RateStat(Rect dest)
        {
            _times = new double[60];
            _timeIndex = 0;

            _text = new Text("0", dest, Position.Center, Color.White);
        }

        internal void Init()
        {
            _text.Init();
        }

        internal void Update() 
        {

        }

        internal void AddTime(double newTime)
        {
            _times[_timeIndex] = newTime;
            _timeIndex = (_timeIndex + 1) % _times.Length;

            Time = _times.Sum() / _times.Length;

            Rate = 1 / Time;

            _text.SetText(Rate.ToString("0.00"));
        }

        internal void Show()
        {
            _text.Show();
        }

        internal void Hide()
        {
            _text.Hide();
        }

    }
}
