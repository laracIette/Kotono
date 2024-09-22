using Kotono.Utils.Exceptions;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Kotono.Audio
{
    internal sealed class Source
    {
        private static readonly Dictionary<string, int> _buffers = [];

        private readonly int _id;

        private float _volume = 1.0f;

        internal float Volume
        {
            get => _volume;
            set
            {
                var newVolume = Math.Clamp(value);
                if (newVolume != _volume)
                {
                    _volume = value;
                    UpdateVolume();
                }
            }
        }

        internal ALSourceState SourceState => AL.GetSourceState(_id);

        internal bool IsPlaying => SourceState == ALSourceState.Playing;

        internal bool IsPaused => SourceState == ALSourceState.Paused;

        internal bool IsStopped => SourceState == ALSourceState.Stopped;

        internal Source(string path)
        {
            if (!_buffers.TryGetValue(path, out var value))
            {
                value = AL.GenBuffer();

                var data = LoadWAV(path, out int channels, out int bits, out int rate);

                nint dataPtr = Marshal.AllocHGlobal(data.Length * sizeof(byte));
                Marshal.Copy(data, 0, dataPtr, data.Length);

                AL.BufferData(value, GetSoundFormat(channels, bits), dataPtr, data.Length, rate);

                _buffers[path] = value;
            }

            _id = AL.GenSource();

            AL.Source(_id, ALSourcei.Buffer, value);

            UpdateVolume();

            AudioManager.UpdateGeneralVolume += OnUpdateGeneralVolume;
        }

        internal void Play() => AL.SourcePlay(_id);

        internal void Pause() => AL.SourcePause(_id);

        internal void Rewind() => AL.SourceRewind(_id);

        internal void Stop() => AL.SourceStop(_id);

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

        private void OnUpdateGeneralVolume(object? sender, EventArgs e) => UpdateVolume();

        private void UpdateVolume() => AL.Source(_id, ALSourcef.Gain, _volume * AudioManager.GeneralVolume);

        private void Delete() => AL.DeleteSource(_id);

        internal void Dispose()
        {
            Stop();
            Delete();
        }

        internal static void DisposeAll()
        {
            foreach (var buffer in _buffers.Values)
            {
                AL.DeleteBuffer(buffer);
            }
        }

        /// <summary>
        /// Creates an array of byte from a WAV file.
        /// </summary>
        /// <exception cref="ArgumentNullException"> The file doesn't exist or isn't found. </exception>
        /// <exception cref="NotSupportedException"> The file isn't a WAVE file. </exception>
        private static byte[] LoadWAV(string filename, out int channels, out int bits, out int rate)
        {
            var stream = File.OpenRead(filename) ?? throw new ArgumentNullException(filename);

            using var reader = new BinaryReader(stream);

            // RIFF header
            var signature = new string(reader.ReadChars(4));
            ExceptionHelper.ThrowIf(signature != "RIFF", "specified stream is not a wave file");

            int riff_chunck_size = reader.ReadInt32();

            var format = new string(reader.ReadChars(4));
            ExceptionHelper.ThrowIf(format != "WAVE", "specified stream is not a wave file");

            // WAVE header
            var format_signature = new string(reader.ReadChars(4));
            ExceptionHelper.ThrowIf(format_signature != "fmt ", "specified wave file is not supported");

            int format_chunk_size = reader.ReadInt32();
            int audio_format = reader.ReadInt16();
            int num_channels = reader.ReadInt16();
            int sample_rate = reader.ReadInt32();
            int byte_rate = reader.ReadInt32();
            int block_align = reader.ReadInt16();
            int bits_per_sample = reader.ReadInt16();

            var data_signature = new string(reader.ReadChars(4));
            ExceptionHelper.ThrowIf(data_signature != "data", "specified wave file is not supported");

            int data_chunk_size = reader.ReadInt32();

            channels = num_channels;
            bits = bits_per_sample;
            rate = sample_rate;

            return reader.ReadBytes((int)reader.BaseStream.Length);
        }

        private static ALFormat GetSoundFormat(int channels, int bits)
        {
            return channels switch
            {
                1 => (bits == 8) ? ALFormat.Mono8 : ALFormat.Mono16,
                2 => (bits == 8) ? ALFormat.Stereo8 : ALFormat.Stereo16,
                _ => throw new NotSupportedException("error: The specified sound format is not supported."),
            };
        }
    }
}
