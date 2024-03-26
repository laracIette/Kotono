using Kotono.Utils.Exceptions;
using System;

namespace Kotono.Utils
{
    internal class Timer : Object
    {
        private bool _isTicking = false;

        private float _startTime = 0.0f;

        private float _targetDuration = 0.0f;

        private float _currentDuration = 0.0f;

        internal event EventHandler<TimeoutEventArgs>? Timeout = null;

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
            _isTicking = true;
            _startTime = Time.Now;
            _currentDuration = duration;
        }


        /// <summary>
        /// Start the timer given a duration.
        /// </summary>
        internal void Start(float duration)
        {
            if (duration > 0.0f)
            {
                Reset(duration);

                _targetDuration = duration;
            }
            else
            {
                throw new KotonoException($"duration \"{duration}\" should be over 0.0f");
            }
        }

        /// <summary>
        /// Start the timer given a duration and wether the timer should restart when timed-out.
        /// </summary>
        internal void Start(float duration, bool isLoop)
        {
            Start(duration);

            IsLoop = isLoop;
        }

        private void OnTimeout()
        {
            Timeout?.Invoke(this, new TimeoutEventArgs());
        }
    }
}
