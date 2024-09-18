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
        private readonly List<Image> _frames = []; // maybe change to ImageTexture[]

        private int _currentFrame = 0;

        private float _lastFrameTime = 0.0f;

        private float _currentPausedTime = 0.0f;

        private float _totalPausedTime = 0.0f;

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

        private float EndTime => CreationTime + StartTime + Duration + _totalPausedTime;

        private float TimeSinceLastFrame => Time.Now - _lastFrameTime;

        private float Delta => 1.0f / FrameRate;

        internal int CurrentFrame
        {
            get => _currentFrame;
            private set
            {
                _frames[_currentFrame].IsDraw = false;
                _currentFrame = (int)Math.Loop(value, _frames.Count);
                _frames[_currentFrame].IsDraw = true;
            }
        }

        internal bool IsPlaying { get; private set; } = false;

        private bool IsPaused => !IsPlaying;

        internal Animation(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new KotonoException($"couldn't find directory at '{directoryPath}'");
            }

            var imagePaths = Directory.GetFiles(directoryPath).Where(ImageTexture.IsValidPath);

            foreach (var filePath in imagePaths)
            {
                _frames.Add(new Image
                {
                    Texture = new ImageTexture(filePath),
                    IsDraw = false,
                    Parent = this
                });
            }

            DirectoryPath = directoryPath;
        }

        public override void Update()
        {
            if (Time.Now > EndTime)
            {
                return;
            }

            if (IsPaused)
            {
                _currentPausedTime += Time.Delta;
                _totalPausedTime += Time.Delta;
            }
            else if (TimeSinceLastFrame - _currentPausedTime >= Delta)
            {
                _lastFrameTime = Time.Now;
                ++CurrentFrame;
                _currentPausedTime = 0.0f;
            }
        }

        internal void Play() => IsPlaying = true;

        internal void Pause() => IsPlaying = false;

        /// <summary>
        /// Revert back to the first frame of the <see cref="Animation"/>, 
        /// without pausing the <see cref="Animation"/>.
        /// </summary>
        internal void Reset()
        {
            CurrentFrame = 0;
            _currentPausedTime = 0.0f;
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

        public override string ToString() => $"Directory: {DirectoryPath}";

        public override void Dispose()
        {
            _frames.ForEach(f => f.Dispose());

            base.Dispose();
        }
    }
}
