using Kotono.Utils;
using OpenTK.Audio.OpenAL;

namespace Kotono.Audio
{
    public class Sound
    {
        private float _volume;

        public int Source { get; set; }

        public float Volume
        {
            get => _volume;
            private set
            {
                _volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }
        
        public bool IsPlaying => AL.GetSourceState(Source) == ALSourceState.Playing;

        public bool IsPaused => AL.GetSourceState(Source) == ALSourceState.Paused;
        
        public bool IsStopped => AL.GetSourceState(Source) == ALSourceState.Stopped;

        public Sound(int source)
        {
            Source = source;
            SetVolume(1.0f);
        }

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

        public void SetVolume(float volume)
        {
            Volume = volume;
            AL.Source(Source, ALSourcef.Gain, Volume * SoundManager.GeneralVolume);
        }
    }
}
