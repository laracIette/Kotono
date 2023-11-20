using Kotono.File;
using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class Animation : IObject2D
    {
        protected readonly List<Image> _frames = new();

        public int Count => _frames.Count;

        public Rect Dest
        {
            get => (Count > 0) ? _frames[0].Dest : throw new Exception("error: cannot access _frames[0].Dest, _frames is empty.");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.Dest = value;
                }
            }
        }

        public int Layer
        {
            get => (Count > 0) ? _frames[0].Layer : throw new Exception("error: cannot access _frames[0].Layer, _frames is empty.");
            set
            {
                foreach (var frame in _frames)
                {
                    frame.Layer = value;
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
                _frames[_currentFrame].Hide();
                _currentFrame = (int)Math.Loop(value, 0, Count);
                _frames[_currentFrame].Show();
            }
        }

        private readonly double _startTime;

        private readonly double _duration;

        private double _lastFrameTime;

        private double _pausedTime = 0;

        private double EndTime => _startTime + _duration + _pausedTime;

        private bool _isStarted = false;

        public bool IsPlaying { get; private set; } = false;

        public bool IsDraw { get; private set; } = true;

        private readonly AnimationProperties _properties;

        /// <summary> Create an Animation from files in a directory </summary>
        public Animation(string path)
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
        }

        public void Init()
        {
            Hide();
        }

        public void Update()
        {
            if (!_isStarted && (Time.NowS >= _startTime))
            {
                _isStarted = true;
                IsPlaying = true;
                _lastFrameTime = Time.NowS;
                _frames[0].Show();
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
        public void Show()
        {
            foreach (var frame in _frames)
            {
                frame.Show();
            }
        }

        public void Hide()
        {
            foreach (var frame in _frames)
            {
                frame.Hide();
            }
        }

        public void Clear()
        {
            foreach (var frame in _frames)
            {
                ObjectManager.DeleteImage(frame);
            }
            _frames.Clear();
        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {

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

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
