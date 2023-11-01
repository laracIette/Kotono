using Kotono.Utils;
using System;

namespace Kotono.Graphics.Objects
{
    public class Animation : IObject2D
    {
        private readonly Image[] _frames;

        private readonly int _frameRate;

        private double DeltaS => 1 / (double)_frameRate;

        private int _currentFrame = 0;

        private double _startTime;

        private double _lastFrameTime;

        private bool _isPlaying = false;

        public Rect Dest { get; set; }

        public int Layer { get; set; }

        public bool IsDraw { get; private set; } = true;

        public Animation(Image[] frames, int frameRate) 
        { 
            _frames = frames;
            _frameRate = frameRate;

            foreach (var frame in _frames)
            {
                frame.Hide();
            }
        }

        public void Init()
        {
        }

        public void Start()
        {
            _startTime = Time.NowS;
            _lastFrameTime = _startTime;
            _isPlaying = true;
        }

        public void Pause()
        {
        }

        public void Update()
        {
            if (_isPlaying)
            {
                if ((Time.NowS - _lastFrameTime) > DeltaS)
                {
                    _lastFrameTime = Time.NowS;

                    _frames[_currentFrame].Hide();
                    _currentFrame = (_currentFrame + 1) % _frames.Length;
                    _frames[_currentFrame].Show();
                }
            }
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

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
