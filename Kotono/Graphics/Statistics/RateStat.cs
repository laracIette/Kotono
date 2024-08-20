using Kotono.Graphics.Objects.Texts;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using System.Linq;

namespace Kotono.Graphics.Statistics
{
    internal class RateStat : Text
    {
        private readonly float[] _times = new float[60];

        private int _timeIndex = 0;

        private int TimeIndex
        {
            get => _timeIndex;
            set => _timeIndex = (int)Math.Loop(value, _times.Length);
        }

        internal float Time { get; private set; }

        internal float Rate => 1.0f / Time;

        public RateStat()
        {
            Size = new Point(50.0f, 60.0f);
            Layer = 1;
            Source = "0.00";
        }

        internal void AddTime(float newTime)
        {
            _times[TimeIndex++] = newTime;

            Time = _times.Average();

            Source = Rate.ToString("0.00");
        }
    }
}
