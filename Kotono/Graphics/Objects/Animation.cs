using Kotono.Graphics.Textures;
using Kotono.Utils;
using Kotono.Utils.Exceptions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal sealed class Animation : Object2D, ISaveable
    {
        private readonly List<Image> _frames = [];

        private int _currentFrame = 0;

        private float _lastFrameTime = 0.0f;

        private float _pausedTime = 0.0f;

        public override int Layer
        {
            get => _frames.FirstOrNull()?.Layer ?? throw new KotonoException("cannot access Layer, _frames is empty");
            set => _frames.ForEach(f => f.Layer = value);
        }

        public override Color Color
        {
            get => _frames.FirstOrNull()?.Color ?? throw new KotonoException("cannot access Color, _frames is empty");
            set => _frames.ForEach(f => f.Color = value);
        }

        /// <summary>
        /// The directory at which the Animation's frames are located.
        /// </summary>
        internal string DirectoryPath { get; }

        internal float Duration { get; set; }

        internal float StartTime { get; set; }

        internal float FrameRate { get; set; }

        internal bool IsLoop { get; set; }

        private float Delta => 1.0f / FrameRate;

        internal int CurrentFrame
        {
            get => _currentFrame;
            private set
            {
                _frames[_currentFrame].IsDraw = false;
                _currentFrame = (int)Math.Loop(value, _frames.Count);
                _frames[_currentFrame].IsDraw = true;
                Logger.Log("update frame", _currentFrame);
            }
        }

        private float TimeSinceLastFrame => Time.Now - _lastFrameTime;

        private float EndTime { get; set; }

        internal bool IsPlaying { get; private set; } = false;

        private bool IsPaused => !IsPlaying;

        internal Animation(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"error: couldn't find directory at '{directoryPath}'");
            }

            var filePaths = Directory.GetFiles(directoryPath);

            foreach (var filePath in filePaths.Where(f => f.EndsWith(".png")))
            {
                _frames.Add(new Image
                {
                    Texture = new ImageTexture(filePath),
                    IsDraw = false,
                    Parent = this
                });
            }

            DirectoryPath = directoryPath;
            EndTime = Time.Now + StartTime + Duration;
        }

        public override void Update()
        {
            if (Time.Now > EndTime)
            {
                return;
            }

            if (IsPaused)
            {
                _pausedTime += Time.Delta;
                EndTime += Time.Delta;
            }
            else if (TimeSinceLastFrame - _pausedTime >= Delta)
            {
                NextFrame();
                UpdateTimes();
            }
        }

        internal void Play() => IsPlaying = true;

        internal void Pause() => IsPlaying = false;

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

        internal void Reset() => CurrentFrame = 0;

        internal void NextFrame() => ++CurrentFrame;

        internal void PreviousFrame() => --CurrentFrame;

        private void UpdateTimes()
        {
            _lastFrameTime = Time.Now;
            _pausedTime = 0.0f;
        }

        public override string ToString() => $"Directory: {DirectoryPath}";

        public override void Dispose()
        {
            _frames.ForEach(f => f.Dispose());

            base.Dispose();
        }
    }
}
