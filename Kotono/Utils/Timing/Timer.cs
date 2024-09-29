using Kotono.Utils.Exceptions;
using System;

namespace Kotono.Utils.Timing
{
    internal sealed class Timer : Object
    {
        private bool _isTicking = false;
        private bool _isPaused = false;

        private float _startTime = 0.0f;
        private float _pauseTime = 0.0f;

        private float _targetDuration = 0.0f;
        private float _currentDuration = 0.0f;

        internal EventHandler<TimeoutEventArgs>? Timeout { get; set; } = null;

        internal bool IsLoop { get; set; } = false;

        private float ElapsedTime => Time.Now - _startTime;

        public override void Update()
        {
            if (_isTicking 
             && !_isPaused 
             && ElapsedTime >= _currentDuration)
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
            _isPaused = false;
            _startTime = Time.Now;
            _currentDuration = duration;
        }

        /// <summary>
        /// Start the timer given a duration.
        /// </summary>
        internal void Start(float duration)
        {
            ExceptionHelper.ThrowIf(duration <= 0.0f, $"duration '{duration}' should be over 0");

            Reset(duration);
            _targetDuration = duration;
        }

        /// <summary>
        /// Start the timer given a duration and whether the timer should restart when timed-out.
        /// </summary>
        internal void Start(float duration, bool isLoop)
        {
            Start(duration);
            IsLoop = isLoop;
        }

        /// <summary>
        /// Pause the timer.
        /// </summary>
        internal void Pause()
        {
            if (!_isTicking || _isPaused)
            {
                return; // Timer is not running or already paused.
            }

            _isPaused = true;
            _pauseTime = Time.Now; // Store the current time when paused.
        }

        /// <summary>
        /// Resume the timer from where it was paused.
        /// </summary>
        internal void Resume()
        {
            if (!_isTicking || !_isPaused)
            {
                return; // Timer is not running or is not paused.
            }

            // Adjust the start time by adding the time spent paused.
            float pausedDuration = Time.Now - _pauseTime;
            _startTime += pausedDuration;
            _isPaused = false;
        }

        internal void Switch()
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void OnTimeout()
            => Timeout?.Invoke(this, new TimeoutEventArgs());
    }
}
