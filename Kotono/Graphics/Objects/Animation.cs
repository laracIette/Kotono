using Kotono.Settings;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace Kotono.Graphics.Objects
{
    internal class Animation : Object2D<AnimationSettings>, ISaveable
    {
        protected readonly List<Image> _frames = [];

        public override Rect Dest
        {
            get => _frames.FirstOrNull()?.Dest ?? throw new KotonoException("cannot access Dest, _frames is empty");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.Dest = value;
                }
            }
        }

        public override int Layer
        {
            get => _frames.FirstOrNull()?.Layer ?? throw new KotonoException("cannot access Layer, _frames is empty");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.Layer = value;
                }
            }
        }

        public override bool IsDraw
        {
            get => _frames.FirstOrNull()?.IsDraw ?? throw new KotonoException("cannot access IsDraw, _frames is empty");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.IsDraw = value;
                }
            }
        }

        private readonly int _frameRate;

        private float Delta => 1.0f / _frameRate;

        private int _currentFrame = 0;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _frames[_currentFrame].IsDraw = false;
                _currentFrame = (int)Math.Loop(value, _frames.Count);
                _frames[_currentFrame].IsDraw = true;
            }
        }

        private readonly float _startTime;

        private readonly float _duration;

        private float _lastFrameTime;

        private float TimeSinceLastFrame => Time.Now - _lastFrameTime;

        private float _pausedTime = 0;

        private float EndTime => _startTime + _duration + _pausedTime;

        private bool _isStarted = false;

        public bool IsPlaying { get; private set; } = false;

        /// <summary> 
        /// Create an Animation from files in a directory.
        /// </summary>
        internal Animation(AnimationSettings settings)
            : base(settings)
        {
            IsDraw = false;

            string[] filePaths;

            string directory = Path.ASSETS + settings.Directory;

            if (Directory.Exists(directory))
            {
                filePaths = Directory.GetFiles(directory);
            }
            else
            {
                throw new DirectoryNotFoundException($"error: couldn't find directory at \"{directory}\"");
            }

            foreach (var filePath in filePaths)
            {
                if (filePath.EndsWith(".png"))
                {
                    _frames.Add(new Image(
                        new ImageSettings
                        {
                            IsDraw = false,
                            Texture = filePath,
                            Dest = settings.Dest,
                            Layer = settings.Layer,
                            Color = settings.Color
                        }
                    ));
                }
            }

            _frameRate = settings.FrameRate;
            _startTime = settings.StartTime;
            _duration = settings.Duration;
        }

        public override void Update()
        {
            if (!_isStarted && (Time.Now >= _startTime))
            {
                _isStarted = true;
                IsPlaying = true;
                _lastFrameTime = Time.Now;
                _frames[0].IsDraw = true;
            }

            if (_isStarted && (Time.Now <= EndTime))
            {
                if (IsPlaying)
                {
                    if ((TimeSinceLastFrame - _pausedTime) >= Delta)
                    {
                        _lastFrameTime = Time.Now - _pausedTime;

                        NextFrame();
                    }
                }
                else
                {
                    _pausedTime += Time.Delta;
                }
            }
        }

        internal void Play()
        {
            IsPlaying = true;
        }

        internal void Pause()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Switch between playing and paused.
        /// </summary>
        internal void Switch()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        internal void NextFrame()
        {
            CurrentFrame++;
        }

        internal void PreviousFrame()
        {
            CurrentFrame--;
        }

        public override string ToString() => $"Directory: {_settings.Directory}";

        public override void Dispose()
        {
            foreach (var frame in _frames)
            {
                frame.Dispose();
            }

            _frames.Clear();

            base.Dispose();
        }
    }
}
