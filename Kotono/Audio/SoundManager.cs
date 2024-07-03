using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IO = System.IO;

namespace Kotono.Audio
{
    internal static class SoundManager
    {
        private static readonly ALDevice _device;

        private static readonly ALContext _context;

        private static readonly Dictionary<string, int> _sources = [];

        private static float _generalVolume = 1.0f;

        internal static float GeneralVolume
        {
            get => _generalVolume;
            set
            {
                _generalVolume = Math.Clamp(value);
            }
        }

        static SoundManager()
        {
            _device = ALC.OpenDevice(null);
            _context = ALC.CreateContext(_device, Array.Empty<int>());

            ALC.MakeContextCurrent(_context);
        }

        internal static int GetSource(string path)
        {
            if (!_sources.TryGetValue(path, out int value))
            {
                int buffer = AL.GenBuffer();
                value = AL.GenSource();

                var data = LoadWAV(path, out int channels, out int bits, out int rate);

                nint dataPtr = Marshal.AllocHGlobal(data.Length * sizeof(byte));
                Marshal.Copy(data, 0, dataPtr, data.Length);

                AL.BufferData(buffer, GetSoundFormat(channels, bits), dataPtr, data.Length, rate);

                AL.Source(value, ALSourcei.Buffer, buffer);

                AL.DeleteBuffer(buffer);

                _sources[path] = value;
            }

            return value;
        }

        /// <summary>
        /// Creates an array of byte from a WAV file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="channels"></param>
        /// <param name="bits"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">The file doesn't exist or isn't found.</exception>
        /// <exception cref="NotSupportedException">The file isn't a WAVE file.</exception>
        private static byte[] LoadWAV(string filename, out int channels, out int bits, out int rate)
        {
            var stream = IO.File.OpenRead(filename) ?? throw new ArgumentNullException(filename);

            using var reader = new IO.BinaryReader(stream);
            // RIFF header
            var signature = new string(reader.ReadChars(4));
            if (signature != "RIFF")
                throw new NotSupportedException("error: Specified stream is not a wave file.");

            int riff_chunck_size = reader.ReadInt32();

            var format = new string(reader.ReadChars(4));
            if (format != "WAVE")
                throw new NotSupportedException("error: Specified stream is not a wave file.");

            // WAVE header
            var format_signature = new string(reader.ReadChars(4));
            if (format_signature != "fmt ")
                throw new NotSupportedException("error: Specified wave file is not supported.");

            int format_chunk_size = reader.ReadInt32();
            int audio_format = reader.ReadInt16();
            int num_channels = reader.ReadInt16();
            int sample_rate = reader.ReadInt32();
            int byte_rate = reader.ReadInt32();
            int block_align = reader.ReadInt16();
            int bits_per_sample = reader.ReadInt16();

            var data_signature = new string(reader.ReadChars(4));
            if (data_signature != "data")
                throw new NotSupportedException("error: Specified wave file is not supported.");

            int data_chunk_size = reader.ReadInt32();

            channels = num_channels;
            bits = bits_per_sample;
            rate = sample_rate;

            return reader.ReadBytes((int)reader.BaseStream.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">The sound format isn't supported.</exception>
        private static ALFormat GetSoundFormat(int channels, int bits)
        {
            return channels switch
            {
                1 => (bits == 8) ? ALFormat.Mono8 : ALFormat.Mono16,
                2 => (bits == 8) ? ALFormat.Stereo8 : ALFormat.Stereo16,
                _ => throw new NotSupportedException("error: The specified sound format is not supported."),
            };
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
