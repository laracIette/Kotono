using Kotono.Graphics.Textures;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using Kotono.Utils.Timing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal sealed class Animation : Object2D, ISaveable
    {
        private readonly List<Image> _frames = []; // TODO: maybe change to ImageTexture[]

        private readonly Timer _timer;

        private int _currentFrame = 0;

        private float _frameRate = 0.0f;

        public override int Layer
        {
            get => base.Layer;
            set
            {
                base.Layer = value;
                _frames.ForEach(frame => frame.Layer = value);
            }
        }

        public override Color Color
        {
            get => base.Color;
            set
            {
                base.Color = value;
                _frames.ForEach(frame => frame.Color = value);
            }
        }

        public override Point RelativeSize
        {
            get => base.RelativeSize;
            set
            {
                base.RelativeSize = value;
                _frames.ForEach(frame => frame.RelativeSize = value);
            }
        }

        /// <summary>
        /// The directory at which the <see cref="Animation"/>'s frames are located.
        /// </summary>
        internal string DirectoryPath { get; }

        private int FrameCount => _frames.Count;

        internal float Duration => FrameCount / FrameRate;

        internal float FrameRate
        {
            get => _frameRate;
            set
            {
                _frameRate = value;
                _timer.TargetDuration = Delta;
            }
        }

        internal bool IsLoop { get; set; } = false;

        private float Delta => 1.0f / FrameRate;

        internal int CurrentFrame
        {
            get => _currentFrame;
            private set
            {
                _frames[_currentFrame].IsDraw = false;
                _currentFrame = (int)Math.Loop(value, FrameCount);
                _frames[_currentFrame].IsDraw = true;
            }
        }

        internal bool IsPlaying { get; private set; }

        internal bool IsPaused => !IsPlaying;

        internal Animation(string directoryPath)
        {
            ExceptionHelper.ThrowIf(
                !Directory.Exists(directoryPath), 
                $"couldn't find directory at '{directoryPath}'"
            );

            var imagePaths = Directory.GetFiles(directoryPath).Where(ImageTexture.IsValidPath);
            
            foreach (var imagePath in imagePaths)
            {
                _frames.Add(new Image
                {
                    Texture = new ImageTexture(imagePath),
                    IsDraw = false,
                    Parent = this,
                });
            }

            _timer = new Timer
            {
                Timeout = (s, e) =>
                {
                    if (!IsLoop && CurrentFrame == FrameCount - 1)
                    {
                        Reset();
                        _frames[CurrentFrame].IsDraw = false;
                    }
                    else
                    {
                        ++CurrentFrame;
                    }
                },
                IsLoop = true,
            };

            DirectoryPath = directoryPath;
        }

        internal void Play()
        {
            Reset();
            IsPlaying = true;
            _timer.Start();
        }

        internal void Pause()
        {
            IsPlaying = false;
            _timer.Pause();
        }

        internal void Resume()
        {
            IsPlaying = true;
            _timer.Resume();
        }

        /// <summary>
        /// Revert back to the first frame of the <see cref="Animation"/>, 
        /// without pausing the <see cref="Animation"/>.
        /// </summary>
        internal void Reset()
        {
            CurrentFrame = 0;
            _timer.Stop();
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

        public override string ToString() 
            => $"{base.ToString()}, Directory: {DirectoryPath}";

        public override void Dispose()
        {
            _frames.ForEach(frame => frame.Dispose());

            base.Dispose();
        }
    }
}
