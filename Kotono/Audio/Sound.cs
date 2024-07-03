using OpenTK.Audio.OpenAL;

namespace Kotono.Audio
{
    internal class Sound(string path) : Object
    {
        private float _volume = 1.0f;

        internal int Source { get; } = SoundManager.GetSource(path);

        internal float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value);
                AL.Source(Source, ALSourcef.Gain, _volume * SoundManager.GeneralVolume);
            }
        }

        internal bool IsPlaying => AL.GetSourceState(Source) == ALSourceState.Playing;

        internal bool IsPaused => AL.GetSourceState(Source) == ALSourceState.Paused;

        internal bool IsStopped => AL.GetSourceState(Source) == ALSourceState.Stopped;

        internal void Play()
        {
            AL.SourcePlay(Source);
        }

        internal void Pause()
        {
            AL.SourcePause(Source);
        }

        internal void Rewind()
        {
            AL.SourceRewind(Source);
        }

        internal void Stop()
        {
            AL.SourceStop(Source);
        }

        public override void Dispose()
        {
            Stop();
            AL.DeleteSource(Source);

            base.Dispose();
        }
    }
}
