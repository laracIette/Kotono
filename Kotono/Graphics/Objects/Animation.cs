using Kotono.File;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class Animation : Object2D, ISaveable
    {
        protected readonly List<Image> _frames = [];

        public int Count => _frames.Count;

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

        private readonly AnimationProperties _properties;

        /// <summary> 
        /// Create an Animation from files in a directory.
        /// </summary>
        public Animation(string path)
            : base()
        {
            _properties = new AnimationProperties(path);

            string[] filePaths;

            if (Directory.Exists(_properties.Directory))
            {
                filePaths = Directory.GetFiles(_properties.Directory);
            }
            else
            {
                throw new DirectoryNotFoundException($"error: couldn't find directory at \"{_properties.Directory}\"");
            }

            foreach (var filePath in filePaths)
            {
                if (filePath.EndsWith(".png"))
                {
                    _frames.Add(new Image(
                        new ImageSettings
                        {
                            Path = filePath,
                            Dest = _properties.Dest,
                            Color = _properties.Color,
                            Layer = _properties.Layer
                        }
                    ));
                }
            }

            _frameRate = _properties.FrameRate;
            _startTime = Time.NowS + _properties.StartTime;
            _duration = _properties.Duration;

            IsDraw = false;
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

        public void Play()
        {
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Next()
        {
            CurrentFrame++;
        }

        public void Previous()
        {
            CurrentFrame--;
        }

        public void Save()
        {
            WriteData();
        }

        private void WriteData()
        {
            _properties.Dest = Dest;
            _properties.Layer = Layer;

            _properties.WriteFile();
        }

        public override string ToString()
        {
            return $"Directory: {_properties["Directory"]}";
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
