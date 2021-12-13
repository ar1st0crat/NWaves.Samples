using Microsoft.Win32;
using NAudio.Wave;
using NWaves.DemoMfccOnline.Interfaces;
using NWaves.DemoMfccOnline.Services;
using NWaves.DemoMfccOnline.Util;
using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Options;
using NWaves.FeatureExtractors.Serializers;
using SciColorMaps.Portable;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NWaves.DemoMfccOnline.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        const int MfccCount = 16;

        const int BitmapWidth = 700;
        const int BitmapHeight = 150;

        public WriteableBitmap MfccBitmap { get; set; }
        public WriteableBitmap WaveformBitmap { get; set; }
        private readonly Bitmap _waveformBitmap;

        private readonly Int32Rect _mfccBitmapRect;
        private readonly byte[] _mfccBitmapClearPixels;
        private readonly Int32Rect _waveformBitmapRect;
        private readonly byte[] _waveformBitmapClearPixels;

        private int _waveformOffset;
        private int _mfccgramOffset;
        private int _mfccgramLength;

        
        const double MinColormapValue = -2;
        const double MaxColormapValue = 2;

        private readonly ColorMap _colormap = new ColorMap("viridis", MinColormapValue, MaxColormapValue);

        
        private readonly IAudioService _audioService;

        public List<float[]> MfccVectors { get; set; } = new List<float[]>();

        public List<string> Devices { get; set; }
        public int DeviceNumber { get; set; }

        
        public DelegateCommand PlayCommand { get; }
        public DelegateCommand PauseCommand { get; }
        public DelegateCommand StopCommand { get; }
        public DelegateCommand StartRecordingCommand { get; }
        public DelegateCommand StopRecordingCommand { get; }
        public DelegateCommand SaveToCsvCommand { get; }


        public MainWindowViewModel()
        {
            _audioService = new AudioService();
            _audioService.VectorsComputed += UpdateMfcc;
            _audioService.WaveformUpdated += UpdateWaveform;

            PlayCommand = new DelegateCommand(Play, () => true);
            PauseCommand = new DelegateCommand(Pause, () => true);
            StopCommand = new DelegateCommand(Stop, () => true);
            StartRecordingCommand = new DelegateCommand(StartRecording, () => true);
            StopRecordingCommand = new DelegateCommand(StopRecording, () => true);
            SaveToCsvCommand = new DelegateCommand(async() => await SaveToCsv(), () => true);

            Devices = Enumerable.Range(0, WaveIn.DeviceCount)
                                .Select(d => $"{d} - {WaveIn.GetCapabilities(d).ProductName}".Replace("Микрофон", "Microphone"))
                                .ToList();

            MfccBitmap = new WriteableBitmap(BitmapWidth, MfccCount, 96, 96, PixelFormats.Bgra32, null);
            WaveformBitmap = new WriteableBitmap(BitmapWidth, BitmapHeight, 96, 96, PixelFormats.Bgra32, null);

            _waveformBitmap = new Bitmap(WaveformBitmap.PixelWidth,
                                         WaveformBitmap.PixelHeight,
                                         WaveformBitmap.BackBufferStride,
                                         System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                                         WaveformBitmap.BackBuffer);
            
            _mfccBitmapRect = new Int32Rect(0, 0, MfccBitmap.PixelWidth, MfccBitmap.PixelHeight);
            _mfccBitmapClearPixels = new byte[MfccBitmap.PixelHeight * MfccBitmap.PixelWidth * 4];
            _waveformBitmapRect = new Int32Rect(0, 0, WaveformBitmap.PixelWidth, WaveformBitmap.PixelHeight);
            _waveformBitmapClearPixels = new byte[WaveformBitmap.PixelHeight * WaveformBitmap.PixelWidth * 4];
        }

        
        #region playback and recording

        void Play()
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == false)
            {
                return;
            }

            _audioService.Load(openFileDialog.FileName);
            
            var mfccOptions = new MfccOptions
            {
                SamplingRate = _audioService.WaveFormat.SampleRate,
                FeatureCount = MfccCount
            };
            var extractor = new MfccExtractor(mfccOptions);
            _audioService.Update(extractor);

            ClearPlots();
            
            _audioService.Play();
        }

        void Pause() => _audioService.Pause();

        void Stop() => _audioService.Stop();


        void StartRecording()
        {
            var mfccOptions = new MfccOptions
            {
                SamplingRate = 16000,
                FeatureCount = MfccCount
            };
            var extractor = new MfccExtractor(mfccOptions);
            _audioService.Update(extractor);

            ClearPlots();

            _audioService.StartRecording(DeviceNumber);
        }

        void StopRecording() => _audioService.StopRecording();

        #endregion


        #region plots

        void UpdateWaveform(float[] waveform)
        {
            WaveformBitmap.Lock();

            using (var bitmapGraphics = System.Drawing.Graphics.FromImage(_waveformBitmap))
            {
                bitmapGraphics.SmoothingMode = SmoothingMode.HighSpeed;
                bitmapGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                bitmapGraphics.CompositingMode = CompositingMode.SourceCopy;
                bitmapGraphics.CompositingQuality = CompositingQuality.HighSpeed;

                var length = waveform.Length / 4;

                var offset = _waveformBitmap.Height / 2;
                var stride = length / _mfccgramLength;

                var i = 0;
                var x = _waveformOffset;
                
                while (i < length)
                {
                    var j = 0;
                    var min = 0.0f;
                    var max = 0.0f;
                    while (j < stride && i + j < length)
                    {
                        if (waveform[i + j] > max) max = waveform[i + j];
                        if (waveform[i + j] < min) min = waveform[i + j];
                        j++;
                    }

                    bitmapGraphics.DrawLine(Pens.MediumPurple, x, -min * offset + offset, x, -max * offset + offset);

                    x++;
                    i += stride;
                }
            }

            WaveformBitmap.AddDirtyRect(new Int32Rect(_waveformOffset, 0, _mfccgramLength, _waveformBitmap.Height));
            WaveformBitmap.Unlock();

            _waveformOffset += _mfccgramLength;
        }

        void UpdateMfcc(List<float[]> mfccVectors)
        {
            if (!mfccVectors.Any())
            {
                return;
            }

            _mfccgramLength = mfccVectors.Count;

            if (_mfccgramOffset + _mfccgramLength > BitmapWidth)
            {
                ClearPlots();
            }

            var rect = new Int32Rect(_mfccgramOffset, 0, mfccVectors.Count, mfccVectors[0].Length);
            
            byte[] pixels = new byte[rect.Height * rect.Width * 4];

            var k = 0;
            for (var y = 0; y < rect.Height; y++)
            {
                for (var x = 0; x < rect.Width; x++)
                {
                    var color = _colormap[mfccVectors[x][MfccCount - 1 - y]];

                    pixels[k++] = color[2];  // B
                    pixels[k++] = color[1];  // G
                    pixels[k++] = color[0];  // R
                    pixels[k++] = 255;       // A
                }
            }

            var stride = rect.Width * 4;

            MfccBitmap.WritePixels(rect, pixels, stride, 0);

            MfccVectors.AddRange(mfccVectors);

            _mfccgramOffset += _mfccgramLength;
        }

        void ClearPlots()
        {
            _mfccgramOffset = 0;
            _waveformOffset = 0;

            // clear bitmaps

            WaveformBitmap.WritePixels(_waveformBitmapRect,
                                       _waveformBitmapClearPixels,
                                       WaveformBitmap.BackBufferStride, 0);

            MfccBitmap.WritePixels(_mfccBitmapRect,
                                   _mfccBitmapClearPixels,
                                   MfccBitmap.BackBufferStride, 0);
        }

        #endregion


        async Task SaveToCsv()
        {
            var sfd = new SaveFileDialog()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (sfd.ShowDialog() != true)
            {
                return;
            }

            using var csvFile = new FileStream(sfd.FileName, FileMode.Create);

            var serializer = new CsvFeatureSerializer(MfccVectors);
            await serializer.SerializeAsync(csvFile);
        }
    }
}
