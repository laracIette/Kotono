using System;

namespace Kotono.Utils
{
    internal class Timer : Object
    {
        private bool _isTicking = false;

        private float _startTime = 0.0f;

        private float _targetDuration = 0.0f;

        private float _currentDuration = 0.0f;

        internal event EventHandler? Timeout = null;

        internal bool IsLoop { get; set; } = false;

        private float ElapsedTime => Time.Now - _startTime;

        public override void Update()
        {
            if (_isTicking && ElapsedTime >= _currentDuration)
            {
                _isTicking = false;

                if (IsLoop)
                {
                    float overtime = ElapsedTime - _currentDuration;

                    Reset(_targetDuration - overtime);
                }

                OnTimeout();
            }
        }

        private void Reset(float duration)
        {
            if (duration > 0.0f)
            {
                _isTicking = true;
                _startTime = Time.Now;
                _currentDuration = duration;
            }
            else
            {
                throw new Exception($"error: duration \"{duration}\" should be over 0.0f.");
            }
        }

        internal void Start(float duration)
        {
            Reset(duration);

            _targetDuration = duration;
        }

        private void OnTimeout()
        {
            Timeout?.Invoke(this, EventArgs.Empty);
        }
    }
}
