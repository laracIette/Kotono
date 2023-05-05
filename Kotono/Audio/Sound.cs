using System;

namespace Kotono.Audio
{
    internal class Sound
    {
        private float _volume = 1.0f;

        internal int Source { get; set; }

        internal float Volume
        {
            get => _volume;
            set
            {
                _volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        internal Sound(int source) 
        { 
            Source = source;
        }
    }
}
