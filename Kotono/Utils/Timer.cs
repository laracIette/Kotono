using Kotono.File;
using System;

namespace Kotono.Utils
{
    internal class Timer : Object
    {
        private bool _isTicking = false;

        private float _startTime = 0.0f;

        private float _duration = 0.0f;

        internal event EventHandler? Timeout = null;

        internal Timer()
            : base(new ObjectSettings()) { }

        public override void Update()
        {
            if (_isTicking && Time.Now - _startTime > _duration)
            {
                OnTimeout();
            }
        }

        internal void Start(float duration)
        {
            if (duration > 0.0f)
            {
                _isTicking = true;
                _startTime = Time.Now;
                _duration = duration;
            }
            else
            {
                throw new Exception($"error: duration ({duration}) should be over 0.0f.");
            }
        }

        private void OnTimeout()
        {
            _isTicking = false;
            Timeout?.Invoke(this, EventArgs.Empty);
        }
    }
}
