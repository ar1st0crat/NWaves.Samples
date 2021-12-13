using NAudio.Wave;
using NWaves.DemoMfccOnline.Interfaces;
using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Base;
using System;
using System.Collections.Generic;

namespace NWaves.DemoMfccOnline.Services
{
    class AudioService : IAudioService
    {
        private AudioFileReader? _reader;
        private WaveOut? _player;
        private WaveIn? _recorder;

        private OnlineFeatureExtractor? _extractor;

        const int MaxBufferSize = 64000;
        private readonly float[] _tmp = new float[MaxBufferSize];

        private WaveFormat? _waveFormat;

        public event Action<float[]>? WaveformUpdated;
        public event Action<List<float[]>>? VectorsComputed;

        public WaveFormat? WaveFormat => _waveFormat;

        public int Channels { get; protected set; }

        public void Load(string filename)
        {
            _player?.Stop();
            _player?.Dispose();
            _reader?.Dispose();

            _reader = new AudioFileReader(filename);

            Channels = _reader.WaveFormat.Channels;
            _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_reader.WaveFormat.SampleRate, Channels);

            _player = new WaveOut();
            _player.Init(this);
        }

        public void Update(MfccExtractor extractor)
        {
            _extractor = new OnlineFeatureExtractor(extractor);
            _extractor.EnsureSizeFromSeconds(2);
        }

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

        public int ReadMono(float[] buffer, int offset, int count)
        {
            if (_reader is null)
            {
                return 0;
            }

            var samplesRead = _reader.Read(buffer, offset, count);

            if (_extractor is null || samplesRead == 0)
            {
                return samplesRead;
            }

            var vectors = _extractor.ComputeFrom(buffer, offset, offset + samplesRead - 1);

            VectorsComputed?.Invoke(vectors);
            WaveformUpdated?.Invoke(buffer);

            return samplesRead;
        }

        public int ReadStereo(float[] buffer, int offset, int count)
        {
            if (_reader is null)
            {
                return 0;
            }

            var samplesRead = _reader.Read(buffer, offset, count);

            if (_extractor is null)
            {
                return samplesRead;
            }

            var pos = 0;
            for (var n = offset; n < count; n += 2)
            {
                _tmp[pos++] = (buffer[n] + buffer[n + 1]) / 2;
            }

            var vectors = _extractor.ComputeFrom(_tmp, 0, pos - 1);

            VectorsComputed?.Invoke(vectors);
            WaveformUpdated?.Invoke(buffer);

            return samplesRead;
        }

        public void Play()
        {
            if (_player is null)
            {
                return;
            }

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

        public void StartRecording(int deviceNumber = 0)
        {
            _recorder = new WaveIn
            {
                WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(16000, 1),
                DeviceNumber = deviceNumber,
                BufferMilliseconds = 200
            };

            _recorder.DataAvailable += OnRecordedDataAvailable;
            
            _recorder?.StartRecording();
        }

        public void StopRecording()
        {
            _recorder?.StopRecording();
            _recorder?.Dispose();
        }

        private void OnRecordedDataAvailable(object? sender, WaveInEventArgs waveInArgs)
        {
            if (_extractor is null)
            {
                return;
            }

            var buffer = new WaveBuffer(waveInArgs.Buffer);

            var size = waveInArgs.BytesRecorded / 4;
            var vectors = _extractor.ComputeFrom(buffer.FloatBuffer, 0, size - 1);

            VectorsComputed?.Invoke(vectors);
            WaveformUpdated?.Invoke(buffer.FloatBuffer);
        }

        public void Dispose()
        {
            _player?.Dispose();
            _reader?.Dispose();
            _recorder?.Dispose();
        }
    }
}
