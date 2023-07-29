using Kotono.Utils;
using OpenTK.Audio.OpenAL;

namespace Kotono.Audio
{
    public class Sound
    {
        private float _volume = 1.0f;

        public int Source { get; private set; }

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

        public Sound(int source)
        {
            Source = source;
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
    }
}
