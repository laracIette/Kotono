using Kotono.Utils;
using System;
using System.Collections.Generic;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    public class Animation : ImageList
    {
        private readonly int _frameRate;

        private double DeltaS => 1 / (double)_frameRate;

        private int _currentFrame = 0;

        public int CurrentFrame
        {
            get => _currentFrame;
            set => _currentFrame = (int)Math.Loop(value, 0, Count);
        }

        private double _startTime;

        private double _lastFrameTime;

        private double _pausedTime = 0;

        public bool IsPlaying { get; private set; } = false;

        public Animation(List<Image> frames, int frameRate) 
            : base(frames)
        { 
            _frameRate = frameRate;
        }

        public void Start()
        {
            _startTime = Time.NowS;
            _lastFrameTime = _startTime;
            _images[0].Show();

            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public override void Update()
        {
            base.Update();

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

        public void Next()
        {
            _images[CurrentFrame++].Hide();
            _images[CurrentFrame].Show();
        }

        public void Previous()
        {
            _images[CurrentFrame--].Hide();
            _images[CurrentFrame].Show();
        }
    }
}
