using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Kotono.Audio
{
    public class SoundManager
    {
        private readonly ALDevice _device;

        private readonly ALContext _context;

        private readonly List<Sound> _sounds = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private float _generalVolume = 1.0f;

        public float GeneralVolume
        {
            get => _generalVolume;
            set
            {
                _generalVolume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }

        private int _soundIndex = 0;

        public SoundManager() 
        {
            _device = ALC.OpenDevice(null);
            _context = ALC.CreateContext(_device, Array.Empty<int>());

            ALC.MakeContextCurrent(_context);
        }

        public int Create(string path)
        {
            int buffer = AL.GenBuffer();
            int source = AL.GenSource();

            var data = LoadWav(path, out int channels, out int bits, out int rate);

            nint dataPtr = Marshal.AllocHGlobal(data.Length * sizeof(byte));
            Marshal.Copy(data, 0, dataPtr, data.Length);

            AL.BufferData(buffer, GetSoundFormat(channels, bits), dataPtr, data.Length, rate);

            AL.Source(source, ALSourcei.Buffer, buffer);

            AL.DeleteBuffer(buffer);

            _indexOffset[_soundIndex] = _sounds.Count;

            _sounds.Add(new Sound(source));
            SetVolume(_soundIndex, GeneralVolume);

            return _soundIndex++;
        }

        public void Delete(int index)
        {
            if (_sounds.Count <= 0)
            {
                throw new Exception($"The number of Sound is already at 0.");
            }

            AL.SourceStop(_sounds[_indexOffset[index]].Source);
            AL.DeleteSource(_sounds[_indexOffset[index]].Source);

            _sounds.RemoveAt(_indexOffset[index]);

            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
        }

        public void Play(int index)
            => AL.SourcePlay(_sounds[_indexOffset[index]].Source);

        public bool IsPlaying(int index) 
            => AL.GetSourceState(_sounds[_indexOffset[index]].Source) == ALSourceState.Playing;

        public void Pause(int index)
            => AL.SourcePause(_sounds[_indexOffset[index]].Source);

        public bool IsPaused(int index)
            => AL.GetSourceState(_sounds[_indexOffset[index]].Source) == ALSourceState.Paused;

        public void Rewind(int index)
            => AL.SourceRewind(_sounds[_indexOffset[index]].Source);

        public void Stop(int index)
            => AL.SourceStop(_sounds[_indexOffset[index]].Source);

        public bool IsStopped(int index)
            => AL.GetSourceState(_sounds[_indexOffset[index]].Source) == ALSourceState.Stopped;

        public float GetVolume(int index)
            => _sounds[_indexOffset[index]].Volume;

        public void SetVolume(int index, float volume)
        {
            _sounds[_indexOffset[index]].Volume = volume;
            AL.Source(_sounds[_indexOffset[index]].Source, ALSourcef.Gain, _sounds[_indexOffset[index]].Volume * GeneralVolume);
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
            var stream = File.OpenRead(filename) ?? throw new ArgumentNullException(filename);

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
            foreach (var sound in _sounds)
            {
                AL.SourceStop(sound.Source);
                AL.DeleteSource(sound.Source);
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
