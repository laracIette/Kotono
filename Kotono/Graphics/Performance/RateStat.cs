﻿using Kotono.Graphics.Objects;
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

        private readonly Text _text = new("0", dest, anchor, Color.White, 1.0f, 1);

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

            Rate = 1.0 / Time;

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
