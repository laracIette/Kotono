using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Path = Kotono.Utils.Path;

namespace Kotono.Audio
{
    public class AudioManager
    {
        private readonly ALDevice _device;

        private readonly ALContext _context;

        private readonly Dictionary<string, int> _sources = new();

        public AudioManager() 
        {
            _device = ALC.OpenDevice(null);
            _context = ALC.CreateContext(_device, Array.Empty<int>());

            ALC.MakeContextCurrent(_context);
        }

        public int Create(string path)
        {
            if (!_sources.ContainsKey(path))
            {
                int buffer = AL.GenBuffer();
                int source = AL.GenSource();

                var data = LoadWav(path, out int channels, out int bits, out int rate);

                nint dataPtr = Marshal.AllocHGlobal(data.Length * sizeof(byte));
                Marshal.Copy(data, 0, dataPtr, data.Length);

                AL.BufferData(buffer, GetSoundFormat(channels, bits), dataPtr, data.Length, rate);

                AL.Source(source, ALSourcei.Buffer, buffer);

                AL.DeleteBuffer(buffer);

                _sources[path] = source;
            }

            return _sources[path];
        }

        public void Play(int source)
        {
            AL.SourcePlay(source);
        }

        public void Pause(int source)
        {
            AL.SourcePause(source);
        }

        public void Rewind(int source)
        {
            AL.SourceRewind(source);
        }

        public void Stop(int source)
        {
            AL.SourceStop(source);
        }

        public void Delete(int source) 
        {
            AL.SourceStop(source);
            AL.DeleteSource(source);

            foreach (var path in _sources.Keys)
            {
                if (_sources[path] == source)
                {
                    _sources.Remove(path);
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="channels"></param>
        /// <param name="bits"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">The file doesn't exist or isn't found.</exception>
        /// <exception cref="NotSupportedException">The file isn't a WAVE file.</exception>
        private static byte[] LoadWav(string filename, out int channels, out int bits, out int rate)
        {
            var stream = File.OpenRead(Path.Assets + filename) ?? throw new ArgumentNullException(filename);

            using (var reader = new BinaryReader(stream))
            {
                // RIFF header
                var signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                var format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                var format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                var data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
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
                _ => throw new NotSupportedException("The specified sound format is not supported."),
            };
        }

        public void Dispose()
        {
            foreach (var source in _sources.Values)
            {
                AL.SourceStop(source);
                AL.DeleteSource(source);
            }

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
