using NAudio.Wave;
using NWaves.DemoStereo.Interfaces;
using NWaves.Effects.Stereo;
using System;

namespace NWaves.DemoStereo.Services
{
    class AudioService : IAudioService
    {
        private AudioFileReader? _reader;
        private WaveOutEvent? _player;
        private StereoEffect? _effect;

        private readonly float[] _tmp = new float[16000];

        private WaveFormat? _waveFormat;
        public WaveFormat? WaveFormat => _waveFormat;

        public int Channels { get; protected set; }

        public float[] Samples
        {
            get
            {
                if (_reader is null)
                {
                    return Array.Empty<float>();
                }

                var tmpReader = new AudioFileReader(_reader.FileName);

                var readBuffer = new float[tmpReader.Length / sizeof(float)];
                tmpReader.Read(readBuffer, 0, readBuffer.Length);
                tmpReader.Close();

                return readBuffer;
            }
        }

        public void Load(string filename)
        {
            _player?.Stop();
            _player?.Dispose();
            _reader?.Dispose();

            _reader = new AudioFileReader(filename);

            Channels = _reader.WaveFormat.Channels;
            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_reader.WaveFormat.SampleRate, 2);

            _player = new WaveOutEvent();
            _player.Init(this);
        }

        public void Update(StereoEffect effect)
        {
            _effect = effect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Read(float[] buffer, int offset, int count)
        {
            if (_reader is null)
            {
                return 0;
            }

            return _reader.WaveFormat.Channels switch
            {
                1 => ReadMono(buffer, offset, count),
                _ => ReadStereo(buffer, offset, count),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int ReadMono(float[] buffer, int offset, int count)
        {
            if (_reader is null)
            {
                return 0;
            }

            var samplesRead = _reader.Read(_tmp, 0, count / 2);

            if (_effect is null)
            {
                return samplesRead;
            }

            var pos = offset;
            for (var n = 0; n < samplesRead; n++)
            {
                _effect.Process(_tmp[n], out float left, out float right);

                buffer[pos++] = left;
                buffer[pos++] = right;
            }

            return samplesRead * 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int ReadStereo(float[] buffer, int offset, int count)
        {
            if (_reader is null)
            {
                return 0;
            }

            var samplesRead = _reader.Read(buffer, offset, count);

            if (_effect is null)
            {
                return samplesRead;
            }

            for (var n = offset; n < samplesRead; n += 2)
            {
                _effect.Process(ref buffer[n], ref buffer[n + 1]);
            }

            return samplesRead;
        }

        public void Play()
        {
            if (_player is null) return;

            if (_player.PlaybackState == PlaybackState.Stopped)
            {
                _reader?.Seek(0, System.IO.SeekOrigin.Begin);
            }

            _player.Play();
        }

        public void Pause()
        {
            _player?.Pause();
        }

        public void Stop()
        {
            _player?.Stop();
            _reader?.Seek(0, System.IO.SeekOrigin.Begin);
        }

        public void Dispose()
        {
            _player?.Dispose();
            _reader?.Dispose();
        }
    }
}
