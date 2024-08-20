using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace Kotono.Graphics.Objects
{
    internal class Animation : Object2D, ISaveable
    {
        protected readonly List<Image> _frames = [];

        public override Rect Rect => _frames.FirstOrNull()?.Rect ?? throw new KotonoException("cannot access Rect, _frames is empty");

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

        public override Color Color
        {
            get => _frames.FirstOrNull()?.Color ?? throw new KotonoException("cannot access Color, _frames is empty");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.Color = value;
                }
            }
        }

        /// <summary>
        /// The directory at which the Animation's frames are.
        /// </summary>
        internal string DirectoryPath { get; set; } = string.Empty;

        internal float Duration { get; set; } = 0.0f;

        internal float StartTime { get; set; } = 0.0f;

        internal float FrameRate { get; set; }

        private float Delta => 1.0f / FrameRate;

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

        private float _lastFrameTime;

        private float TimeSinceLastFrame => Time.Now - _lastFrameTime;

        private float _pausedTime = 0;

        private float EndTime => StartTime + Duration + _pausedTime;

        private bool _isStarted = false;

        public bool IsPlaying { get; private set; } = false;

        [JsonConstructor]
        internal Animation() { }

        internal Animation(string directoryPath) : this()
        {
            DirectoryPath = directoryPath;

            if (Directory.Exists(DirectoryPath))
            {
                var filePaths = Directory.GetFiles(DirectoryPath);

                foreach (var filePath in filePaths)
                {
                    if (filePath.EndsWith(".png"))
                    {
                        _frames.Add(new Image(filePath));
                    }
                }
            }
            else
            {
                throw new DirectoryNotFoundException($"error: couldn't find directory at \"{DirectoryPath}\"");
            }
        }

        public override void Update()
        {
            if (!_isStarted && (Time.Now >= StartTime))
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

        internal void NextFrame() => CurrentFrame++;

        internal void PreviousFrame() => CurrentFrame--;

        public override string ToString() => $"Directory: {DirectoryPath}";

        public override void Dispose()
        {
            foreach (var frame in _frames)
            {
                frame.Dispose();
            }

            base.Dispose();
        }
    }
}
