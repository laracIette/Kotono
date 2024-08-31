using OpenTK.Audio.OpenAL;

namespace Kotono.Audio
{
    internal class Sound : Object
    {
        private int _source = InvalidSource;

        private float _volume = 1.0f;

        internal float Volume
        {
            get => _volume;
            set
            {
                if (_source == InvalidSource)
                {
                    logError($"Invalid Source");
                    return;
                }

                _volume = Math.Clamp(value);
                UpdateVolume();
            }
        }

        internal bool IsPlaying => AL.GetSourceState(_source) == ALSourceState.Playing;

        internal bool IsPaused => AL.GetSourceState(_source) == ALSourceState.Paused;

        internal bool IsStopped => AL.GetSourceState(_source) == ALSourceState.Stopped;

        internal static int InvalidSource => -1;

        internal void SetSource(string path)
        {
            _source = SoundManager.GetSource(path);
            UpdateVolume();
        }

        internal void Play()
        {
            AL.SourcePlay(_source);
        }

        internal void Pause()
        {
            AL.SourcePause(_source);
        }

        internal void Rewind()
        {
            AL.SourceRewind(_source);
        }

        internal void Stop()
        {
            AL.SourceStop(_source);
        }

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

        private void UpdateVolume()
        {
            AL.Source(_source, ALSourcef.Gain, Volume * SoundManager.GeneralVolume);
        }

        public override void Dispose()
        {
            Stop();
            AL.DeleteSource(_source);

            base.Dispose();
        }
    }
}
