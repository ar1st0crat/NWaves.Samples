using NAudio.Wave;
using NWaves.Effects.Stereo;
using System;

namespace NWaves.DemoStereo.Interfaces
{
    public interface IAudioService : ISampleProvider, IDisposable
    {
        void Play();
        void Pause();
        void Stop();

        float[] Samples { get; }
        int Channels { get; }

        void Load(string filename);
        void Update(StereoEffect effect);
    }
}
