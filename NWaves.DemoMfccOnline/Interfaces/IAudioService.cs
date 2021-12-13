using NAudio.Wave;
using NWaves.FeatureExtractors;
using System;
using System.Collections.Generic;

namespace NWaves.DemoMfccOnline.Interfaces
{
    public interface IAudioService : ISampleProvider, IDisposable
    {
        int Channels { get; }

        void Play();
        void Pause();
        void Stop();
        void StartRecording(int deviceNumber);
        void StopRecording();

        void Load(string filename);
        void Update(MfccExtractor extractor);

        event Action<float[]> WaveformUpdated;
        event Action<List<float[]>> VectorsComputed;
    }
}
