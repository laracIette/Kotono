using Kotono.File;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Math = Kotono.Utils.Math;
using Path = Kotono.Utils.Path;

namespace Kotono.Graphics.Objects
{
    internal class Animation : Object2D, ISaveable
    {
        protected readonly List<Image> _frames = [];

        internal int Count => _frames.Count;

        public override Rect Dest
        {
            get => _frames.FirstOrDefault()?.Dest ?? throw new Exception("error: cannot access Dest, _frames is empty.");
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
            get => _frames.FirstOrDefault()?.Layer ?? throw new Exception("error: cannot access Layer, _frames is empty.");
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
            get => _frames.FirstOrDefault()?.IsDraw ?? throw new Exception("error: cannot access IsDraw, _frames is empty.");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.IsDraw = value;
                }
            }
        }

        private readonly int _frameRate;

        private double DeltaS => 1 / (double)_frameRate;

        private int _currentFrame = 0;

        private int CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _frames[_currentFrame].IsDraw = false;
                _currentFrame = (int)Math.Loop(value, Count);
                _frames[_currentFrame].IsDraw = true;
            }
        }

        private readonly double _startTime;

        private readonly double _duration;

        private double _lastFrameTime;

        private double _pausedTime = 0;

        private double EndTime => _startTime + _duration + _pausedTime;

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
            _startTime = Time.NowS + settings.StartTime;
            _duration = settings.Duration;
        }

        public override void Update()
        {
            if (!_isStarted && (Time.NowS >= _startTime))
            {
                _isStarted = true;
                IsPlaying = true;
                _lastFrameTime = Time.NowS;
                _frames[0].IsDraw = true;
            }

            if (_isStarted && (Time.NowS <= EndTime))
            {
                if (IsPlaying)
                {
                    if ((Time.NowS - _pausedTime - _lastFrameTime) > DeltaS)
                    {
                        _lastFrameTime = Time.NowS - _pausedTime;

                        Next();
                    }
                }
                else
                {
                    _pausedTime += Time.DeltaS;
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

        internal void Next()
        {
            CurrentFrame++;
        }

        internal void Previous()
        {
            CurrentFrame--;
        }

        public void Save()
        {
            WriteData();
        }

        private void WriteData()
        {
            ((AnimationSettings)_settings).Dest = Dest;
            ((AnimationSettings)_settings).Layer = Layer;
            ((AnimationSettings)_settings).IsDraw = IsDraw;

            Settings.WriteFile(_settings);
        }

        public override string ToString()
        {
            return $"Directory: {((AnimationSettings)_settings).Directory}";
        }

        public override void Delete()
        {
            foreach (var frame in _frames)
            {
                frame.Delete();
            }

            _frames.Clear();

            base.Delete();
        }
    }
}
