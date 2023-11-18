using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using System;
using System.Collections.Generic;
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

        public int CurrentFrame
        {
            get => _currentFrame;
            private set 
            {
                _frames[_currentFrame].Hide();
                _currentFrame = (int)Math.Loop(value, 0, Count); 
                _frames[_currentFrame].Show();
            }
        }

        private double _startTime;

        private double _lastFrameTime;

        private double _pausedTime = 0;

        public bool IsPlaying { get; private set; } = false;

        public bool IsDraw { get; private set; } = true;

        public Animation(List<Image> frames, int frameRate)
        { 
            _frames = frames;
            _frameRate = frameRate;
        }

        public void Init()
        {
            Hide();
        }

        public void Update()
        {
            if (IsPlaying)
            {
                if ((Time.NowS - (_lastFrameTime + _pausedTime)) > DeltaS)
                {
                    _lastFrameTime = Time.NowS;
                    _pausedTime = 0;

                    Next();
                }
            }
            else
            {
                //_pausedTime += Time.DeltaS;
            }
        }

        public void Play()
        {
            _startTime = Time.NowS;
            _lastFrameTime = _startTime;
            CurrentFrame = 0;

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

        }

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}
