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

        private float _currentDuration = 0.0f;

        internal EventHandler<TimeoutEventArgs>? Timeout { get; set; } = null;

        internal bool IsLoop { get; set; } = false;

        internal float TargetDuration { get; set; } = 0.0f;

        internal float ElapsedTime => Time.Now - _startTime;

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
                    Reset(TargetDuration - overtime);
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
        /// Start the timer.
        /// </summary>
        internal void Start()
        {
            ExceptionHelper.ThrowIf(
                TargetDuration <= 0.0f, 
                $"duration '{TargetDuration}' should be over 0"
            );

            Reset(TargetDuration);
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

        internal void Stop()
        {
            _isTicking = false;
            _isPaused = false;
            _startTime = 0.0f;
            _currentDuration = 0.0f;
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
