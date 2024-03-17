using OpenTK.Audio.OpenAL;
using System;

namespace Kotono.Audio
{
    public class Sound(string path) : IDisposable
    {
        private float _volume = 1.0f;

        public int Source { get; } = SoundManager.GetSource(path);

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0.0f, 1.0f);
                AL.Source(Source, ALSourcef.Gain, _volume * SoundManager.GeneralVolume);
            }
        }

        public bool IsPlaying => AL.GetSourceState(Source) == ALSourceState.Playing;

        public bool IsPaused => AL.GetSourceState(Source) == ALSourceState.Paused;

        public bool IsStopped => AL.GetSourceState(Source) == ALSourceState.Stopped;

        public void Play()
        {
            AL.SourcePlay(Source);
        }

        public void Pause()
        {
            AL.SourcePause(Source);
        }

        public void Rewind()
        {
            AL.SourceRewind(Source);
        }

        public void Stop()
        {
            AL.SourceStop(Source);
        }

        public void Dispose()
        {
            Stop();
            AL.DeleteSource(Source);

            GC.SuppressFinalize(this);
        }
    }
}
