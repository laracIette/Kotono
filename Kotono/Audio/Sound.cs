using System;

namespace Kotono.Audio
{
    public class Sound
    {
        private float _volume = 1.0f;

        public int Source { get; set; }

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        public Sound(int source) 
        { 
            Source = source;
        }
    }
}
