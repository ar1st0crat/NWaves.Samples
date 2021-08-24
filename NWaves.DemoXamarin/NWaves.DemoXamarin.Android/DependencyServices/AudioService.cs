#define AUDIO_PCM_FLOAT

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Media;
using NWaves.Audio;
using NWaves.DemoXamarin.DependencyServices;
using NWaves.Effects;
using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Base;
using NWaves.FeatureExtractors.Options;
using NWaves.Features;
using NWaves.Filters.Base;
using NWaves.Signals;
using Xamarin.Forms;

[assembly: Dependency(typeof(NWaves.DemoXamarin.Droid.DependencyServices.AudioService))]
namespace NWaves.DemoXamarin.Droid.DependencyServices
{
#if AUDIO_PCM_FLOAT

    public class AudioService : IAudioService
    {
        private int _samplingRate;

        private readonly ChannelIn _channelCount = ChannelIn.Mono;
        private readonly Encoding _audioEncodingType = Encoding.PcmFloat;  // not available for old Android versions
        
        private AudioRecord _recorder;

        private int _bufferSize;
        private int _sizeInFloats;
        private byte[] _bytes;
        private float[][] _data;    // array of samples in each channel
        private bool _isRecording;

        private List<float[]> _pitches;
        private readonly PitchEstimatedEventArgs _pitchArgs = new PitchEstimatedEventArgs();
        public event EventHandler<PitchEstimatedEventArgs> PitchEstimated;

        private OnlineFeatureExtractor _pitchExtractor;
        private IOnlineFilter _robotizer;


        public AudioService()
        {
        }

        public async void StartRecording()
        {
            if (_recorder != null)
            {
                StopRecording();
            }

            var context = Android.App.Application.Context;
            var audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            _samplingRate = int.Parse(audioManager.GetProperty(AudioManager.PropertyOutputSampleRate));

            _bufferSize = 4 * AudioRecord.GetMinBufferSize(_samplingRate, ChannelIn.Mono, Encoding.PcmFloat);
            _sizeInFloats = _bufferSize / sizeof(float);

            _bytes = new byte[_bufferSize];

            _data = new float[1][];
            _data[0] = new float[_sizeInFloats];    // only one channel (mono)


            _recorder = new AudioRecord(AudioSource.Mic, _samplingRate, _channelCount, _audioEncodingType, _bufferSize);


            var options = new PitchOptions
            {
                SamplingRate = _samplingRate,
                FrameDuration = 0.032/*sec*/,
                HopDuration = 0.032/*sec*/
            };
            _pitchExtractor = new OnlineFeatureExtractor(new PitchExtractor(options));

            // reserve memory for pitch values
            // (this memory will be efficiently reused during processing each audio block)

            var pitchVectorsPerBuffer = _pitchExtractor.VectorCount(_sizeInFloats);

            _pitches = new List<float[]>(pitchVectorsPerBuffer);

            for (var i = 0; i < pitchVectorsPerBuffer; i++)
            {
                _pitches.Add(new float[1]);
            }

            //

            _robotizer = new RobotEffect(216, 1024);

            _recorder.StartRecording();
            _isRecording = true;

            await ProcessAudioData();
        }

        public void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }

            _isRecording = false;

            _recorder.Stop();
            _recorder.Release();
            _recorder = null;
        }

        private async Task ProcessAudioData()
        {
            var data = _data[0];

            var filename = TempFileName;

            using (var tempStream = new FileStream(filename, FileMode.Create))
            {
                // ==================================== main recording loop ========================================

                while (_isRecording)
                {
                    await _recorder.ReadAsync(data, 0, _sizeInFloats, 0);

                    // 1) run feature extractor:

                    _pitchExtractor.ComputeFrom(data, _pitches);

                    _pitchArgs.PitchZcr = Pitch.FromZeroCrossingsSchmitt(data, _samplingRate, 0, _sizeInFloats);
                    _pitchArgs.PitchAutoCorr = _pitches.Max(p => p[0]);

                    PitchEstimated(this, _pitchArgs);       // raise event (GUI is subscribed)


                    // 2) apply robotize effect:

                    _robotizer.Process(data, data);


                    // write data to output file

                    Buffer.BlockCopy(data, 0, _bytes, 0, _bufferSize);
                    await tempStream.WriteAsync(_bytes, 0, _bufferSize);
                }
            }

            SaveToFile();
        }

        private void SaveToFile()
        {
            using (var tempStream = new FileStream(TempFileName, FileMode.Open))
            using (var br = new BinaryReader(tempStream))
            {
                var samples = new float[tempStream.Length / sizeof(float)];

                for (var i = 0; i < samples.Length; i++)
                {
                    samples[i] = br.ReadSingle();
                }

                var waveFile = new WaveFile(new DiscreteSignal(_samplingRate, samples));

                using var outputStream = new FileStream(OutputFileName, FileMode.Create);

                waveFile.SaveTo(outputStream);
            }

            using var file = new Java.IO.File(TempFileName);

            file.Delete();
        }

        private string TempFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "temp.wav");

        private string OutputFileName => Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android/media", "recorded.wav");
    }

#else

    /// <summary>
    /// Alternative version:
    ///     
    ///     - PCM 16 bit
    ///     - Feature vectors (pitches) are accumulated in memory
    /// 
    /// </summary>
    public class AudioService : IAudioService
    {
        private int _samplingRate;

        private readonly ChannelIn _channelCount = ChannelIn.Mono;
        private readonly Encoding _audioEncodingType = Encoding.Pcm16bit;

        private AudioRecord _recorder;

        private int _bufferSize;
        private int _sizeInFloats;
        private byte[] _bytes, _temp;
        private float[][] _data;    // array of samples in each channel
        private bool _isRecording;

        private readonly PitchEstimatedEventArgs _pitchArgs = new PitchEstimatedEventArgs();
        public event EventHandler<PitchEstimatedEventArgs> PitchEstimated;

        private OnlineFeatureExtractor _pitchExtractor;
        private IOnlineFilter _robotizer;


        public AudioService()
        {
        }

        public async void StartRecording()
        {
            if (_recorder != null)
            {
                StopRecording();
            }

            var context = Android.App.Application.Context;
            var audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            _samplingRate = int.Parse(audioManager.GetProperty(AudioManager.PropertyOutputSampleRate));

            _bufferSize = 4 * AudioRecord.GetMinBufferSize(_samplingRate, ChannelIn.Mono, Encoding.Pcm16bit);
            _sizeInFloats = _bufferSize / sizeof(short);

            _data = new float[1][];
            _data[0] = new float[_sizeInFloats];    // only one channel (mono)

            _bytes = new byte[_bufferSize];
            _temp = new byte[_sizeInFloats * sizeof(float)];

            
            _recorder = new AudioRecord(AudioSource.Mic, _samplingRate, _channelCount, _audioEncodingType, _bufferSize);


            var options = new PitchOptions
            {
                SamplingRate = _samplingRate,
                FrameDuration = 0.032/*sec*/,
                HopDuration = 0.032/*sec*/
            };
            _pitchExtractor = new OnlineFeatureExtractor(new PitchExtractor(options));


            _robotizer = new RobotEffect(216, 1024);

            _recorder.StartRecording();
            _isRecording = true;

            await ProcessAudioData();
        }

        public void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }

            _isRecording = false;

            _recorder.Stop();
            _recorder.Release();
            _recorder = null;
        }

        private async Task ProcessAudioData()
        {
            var data = _data[0];
            
            var filename = TempFileName;

            using (var tempStream = new FileStream(filename, FileMode.Create))
            {
                // ==================================== main recording loop ========================================

                while (_isRecording)
                {
                    await _recorder.ReadAsync(_bytes, 0, _bufferSize);
                    ByteConverter.ToFloats16Bit(_bytes, _data);


                    // 1) run feature extractor:

                    _pitchArgs.PitchZcr = Pitch.FromZeroCrossingsSchmitt(data, _samplingRate, 0, _sizeInFloats);
                    _pitchArgs.PitchAutoCorr = _pitchExtractor.ComputeFrom(data).Max(p => p[0]);

                    PitchEstimated(this, _pitchArgs);       // raise event (GUI is subscribed)


                    // 2) apply robotize effect:

                    _robotizer.Process(data, data);

                    Buffer.BlockCopy(data, 0, _temp, 0, _temp.Length);
                    await tempStream.WriteAsync(_temp, 0, _temp.Length);
                }
            }

            SaveToFile();
        }

        private void SaveToFile()
        {
            using (var tempStream = new FileStream(TempFileName, FileMode.Open))
            using (var br = new BinaryReader(tempStream))
            {
                var samples = new float[tempStream.Length / sizeof(float)];

                for (var i = 0; i < samples.Length; i++)
                {
                    samples[i] = br.ReadSingle();
                }

                var waveFile = new WaveFile(new DiscreteSignal(_samplingRate, samples));

                using var outputStream = new FileStream(OutputFileName, FileMode.Create);

                waveFile.SaveTo(outputStream);
            }

            using var file = new Java.IO.File(TempFileName);

            file.Delete();
        }

        private string TempFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "temp.wav");

        private string OutputFileName => Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android/media", "recorded.wav");
    }
#endif

}
