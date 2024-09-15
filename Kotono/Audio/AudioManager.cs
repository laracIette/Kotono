using OpenTK.Audio.OpenAL;
using System;

namespace Kotono.Audio
{
    internal static class AudioManager
    {
        private static readonly ALDevice _device;

        private static readonly ALContext _context;

        private static float _generalVolume;

        internal static float GeneralVolume
        {
            get => _generalVolume;
            set
            {
                var newVolume = Math.Clamp(value);
                if (newVolume != _generalVolume)
                {
                    _generalVolume = newVolume;
                    UpdateGeneralVolume?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        internal static EventHandler? UpdateGeneralVolume { get; set; } = null;

        static AudioManager()
        {
            _device = ALC.OpenDevice(null);
            _context = ALC.CreateContext(_device, Array.Empty<int>());

            ALC.MakeContextCurrent(_context);

            GeneralVolume = 1.0f;
        }

        internal static void Dispose()
        {
            if (_context != ALContext.Null)
            {
                ALC.MakeContextCurrent(ALContext.Null);
                ALC.DestroyContext(_context);
            }

            if (_device != ALDevice.Null)
            {
                ALC.CloseDevice(_device);
            }
        }
    }
}
