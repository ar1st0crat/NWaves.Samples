using Caliburn.Micro;
using Microsoft.Win32;
using NWaves.Audio;
using NWaves.DemoStereo.Interfaces;
using NWaves.Effects;
using NWaves.Effects.Stereo;
using NWaves.Signals;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace NWaves.DemoStereo.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly IAudioService _audioService;

        /// <summary>
        /// Simple panning is turned on by default
        /// </summary>
        private readonly PanEffect _panEffect = new PanEffect(0, PanRule.Linear);
        
        /// <summary>
        /// Optional ITD-ILD panning (controlled by the corresponding checkbox)
        /// </summary>
        private ItdIldPanEffect? _panItdEffect;
        //private StereoDelayEffect? _panItdEffect;

        /// <summary>
        /// Optional binaural panning
        /// </summary>
        private BinauralPanEffect? _binauralPanEffect;

        /// <summary>
        /// Azimuths (CIPIC)
        /// </summary>
        private readonly float[] _azimuths = { -80, -65, -55, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 55, 65, 80 };

        /// <summary>
        /// Azimuths (CIPIC)
        /// </summary>
        private readonly float[] _elevations = { -45, -39, -34, -28, -23, -17, -11, -6, 0, 6, 11, 17, 23, 28, 34, 39,
                                                  45, 51, 56, 62, 68, 73, 79, 84, 90, 96, 101, 107, 113, 118, 124, 129,
                                                  135, 141, 146, 152, 158, 163, 169, 174, 180, 186, 191, 197, 203, 208, 214, 219, 225, 231};

        private float _pan;
        public float Pan
        {
            get => _pan;
            set
            {
                _pan = (float)Math.Round(value, 2);
                _panEffect.Pan = _pan;
                
                if (_panItdEffect != null)
                {
                    _panItdEffect.Pan = _pan;
                }

                NotifyOfPropertyChange(() => Pan);
            }
        }

        private PanRule _panRule;
        public PanRule PanRule
        {
            get => _panRule;
            set
            {
                _panRule = value;
                _panEffect.PanRule = _panRule;
                NotifyOfPropertyChange(() => PanRule);
            }
        }

        private bool _itdIld;
        public bool ItdIld
        {
            get => _itdIld;
            set
            {
                _itdIld = value;
                NotifyOfPropertyChange(() => ItdIld);
                NotifyOfPropertyChange(() => CanPanRules);
            }
        }

        public bool CanPanRules => !ItdIld;

        private float _azimuth;
        public float Azimuth
        {
            get => _azimuth;
            set
            {
                _azimuth = value;

                if (_binauralPanEffect != null)
                {
                    _binauralPanEffect.Azimuth = _azimuth;
                }

                UpdateDirectionPoint(_azimuth, _elevation);
                NotifyOfPropertyChange(() => Azimuth);
            }
        }

        private float _elevation;
        public float Elevation
        {
            get => _elevation;
            set
            {
                _elevation = value;

                if (_binauralPanEffect != null)
                {
                    _binauralPanEffect.Elevation = _elevation;
                }

                UpdateDirectionPoint(_azimuth, _elevation);
                NotifyOfPropertyChange(() => Elevation);
            }
        }

        private int _crossoverFrequency = 150; /*Hz*/
        public int CrossoverFrequency
        { 
            get => _crossoverFrequency;
            set
            {
                _crossoverFrequency = value;

                if (_binauralPanEffect != null)
                {
                    _binauralPanEffect.SetCrossoverParameters(_crossoverFrequency, _audioService.WaveFormat.SampleRate);
                }

                NotifyOfPropertyChange(() => CrossoverFrequency);
            }
        }

        private bool _useCrossover;
        public bool UseCrossover
        {
            get => _useCrossover;
            set
            {
                _useCrossover = value;

                if (_binauralPanEffect != null)
                {
                    _binauralPanEffect?.SetCrossoverParameters(_crossoverFrequency, _audioService.WaveFormat.SampleRate);
                    _binauralPanEffect?.UseCrossover(_useCrossover);
                }

                NotifyOfPropertyChange(() => UseCrossover);
            }
        }


        public MainWindowViewModel(IAudioService audioService)
        {
            _audioService = audioService;

            InitBinauralPlot();
        }

        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == false)
            {
                return;
            }

            _audioService.Load(openFileDialog.FileName);
        }

        /// <summary>
        /// Play audio with pan effect:
        /// 
        ///     - if user checked ITD-ILD checkbox, then ITD-ILD panning is applied
        ///     - otherwise, if user loaded HRIRs, then binaural panning is applied
        ///     - otherwise, simple stereo panning is applied
        ///     
        /// </summary>
        public void Play()
        {
            if (!ItdIld)
            {
                if (_binauralPanEffect is null)
                {
                    _audioService.Update(_panEffect);
                }
                else
                {
                    _audioService.Update(_binauralPanEffect);
                }
            }
            else
            {
                // unlike simple panning, ITD-ILD panner depends on a sampling rate,
                // so it needs to be instantiated separately:

                _panItdEffect = new ItdIldPanEffect(_audioService.WaveFormat.SampleRate, _pan);
                //_panItdEffect = new StereoDelayEffect(_audioService.WaveFormat.SampleRate, _pan, 0.13f, 0.76f);
                _audioService.Update(_panItdEffect);
            }

            _audioService.Play();
        }

        public void Stop()
        {
            _audioService.Stop();
        }

        /// <summary>
        /// We're saving output WAVE files using NWaves
        /// after reading audio data from input files using NAudio
        /// </summary>
        public void SaveFile()
        {
            var saveFileDialog = new SaveFileDialog();
            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == false)
            {
                return;
            }

            StereoEffect effect;

            if (ItdIld && _panItdEffect != null)
            {
                effect = _panItdEffect;
            }
            else if (_binauralPanEffect != null)
            {
                effect = _binauralPanEffect;
            }
            else
            {
                effect = _panEffect;
            }


            (DiscreteSignal Left, DiscreteSignal Right) signals;

            if (_audioService.Channels == 1)
            {
                var signal = new DiscreteSignal(_audioService.WaveFormat.SampleRate, _audioService.Samples);

                signals = effect.ApplyTo(signal);
            }
            else
            {
                var samples = _audioService.Samples;

                var leftSamples = samples.Where((item, index) => index % 2 == 0);
                var rightSamples = samples.Where((item, index) => index % 2 == 1);
                
                var left = new DiscreteSignal(_audioService.WaveFormat.SampleRate, leftSamples);
                var right = new DiscreteSignal(_audioService.WaveFormat.SampleRate, rightSamples);

                signals = effect.ApplyTo(left, right);
            }
            

            using var stream = new FileStream(saveFileDialog.FileName, FileMode.Create);

            var waveFile = new WaveFile(new[] { signals.Left, signals.Right });
            waveFile.SaveTo(stream);
        }


        /// <summary>
        /// Read HRIRs from CIPIC files
        /// </summary>
        public void LoadHrirs()
        {
            var openFileDialog = new OpenFileDialog();
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == false)
            {
                return;
            }

            var directory = Path.GetDirectoryName(openFileDialog.FileName);
            
            if (directory is null) return;

            var leftHrirs = new float[_azimuths.Length][][];
            var rightHrirs = new float[_azimuths.Length][][];

            for (var i = 0; i < leftHrirs.Length; i++)
            {
                leftHrirs[i] = new float[_elevations.Length][];
                rightHrirs[i] = new float[_elevations.Length][];

                var leftHrirFilename = Path.Combine(directory, $"{_azimuths[i]}azleft.wav".Replace("-", "neg"));
                var rightHrirFilename = Path.Combine(directory, $"{_azimuths[i]}azright.wav".Replace("-", "neg"));

                for (var j = 0; j < _elevations.Length; j++)
                {
                    using var streamLeft = new FileStream(leftHrirFilename, FileMode.Open);
                    var waveFileLeft = new WaveFile(streamLeft);

                    leftHrirs[i][j] = new float [waveFileLeft.WaveFmt.ChannelCount];

                    for (var k = 0; k < leftHrirs[i][j].Length; k++)
                    {
                        leftHrirs[i][j][k] = waveFileLeft.Signals[k].Samples[j];
                    }

                    using var streamRight = new FileStream(rightHrirFilename, FileMode.Open);
                    var waveFileRight = new WaveFile(streamRight);

                    rightHrirs[i][j] = new float[waveFileRight.WaveFmt.ChannelCount];

                    for (var k = 0; k < rightHrirs[i][j].Length; k++)
                    {
                        rightHrirs[i][j][k] = waveFileRight.Signals[k].Samples[j];
                    }
                }
            }

            _binauralPanEffect = new BinauralPanEffect(_azimuths, _elevations, leftHrirs, rightHrirs)
            {
                Azimuth = _azimuth,
                Elevation = _elevation
            };
        }


        #region binaural plot

        public ObservableCollection<BinauralPlotPoint> BinauralPlotPoints { get; set; } = 
            new ObservableCollection<BinauralPlotPoint>();

        const int CenterX = 200;
        const int CenterY = 135;

        const float R = 130;

        private void InitBinauralPlot()
        {
            foreach (var theta in _azimuths)
            {
                foreach (var phi in _elevations)
                {
                    BinauralPlotPoints.Add(CalculatePoint(theta, phi));
                }
            }

            AddHeadPoint();
            UpdateDirectionPoint(0, 0);
        }

        private void AddHeadPoint()
        {
            BinauralPlotPoints.Add(new BinauralPlotPoint
            { 
                X = CenterX - 28,
                Y = CenterY - 28,
                Type = 2
            });
        }

        private void UpdateDirectionPoint(float azimuth, float elevation)
        {
            var lastPoint = BinauralPlotPoints.Last();
            if (lastPoint.Type == 1)
            {
                BinauralPlotPoints.Remove(lastPoint);
            }

            BinauralPlotPoints.Add(CalculatePoint(azimuth, elevation, 1));
        }

        public static BinauralPlotPoint CalculatePoint(float azimuth, float elevation, int type = 0)
        {
            var x = R * Math.Cos(azimuth * Math.PI / 180) * Math.Cos(elevation * Math.PI / 180);
            var y = R * Math.Sin(azimuth * Math.PI / 180) * Math.Cos(elevation * Math.PI / 180);
            var z = R * Math.Sin(elevation * Math.PI / 180);

            var ys = (x - z) / Math.Sqrt(2);
            var xs = (x + 2 * y + z) / Math.Sqrt(6);

            var point = new BinauralPlotPoint
            {
                X = xs + CenterX,
                Y = ys + CenterY,
                Type = type
            };

            switch (type)
            {
                case 0:
                    point.X -= 1;
                    point.Y -= 1;
                    break;
                case 1:
                    point.X -= 14;
                    point.Y -= 14;
                    break;
            }

            return point;
        }

        #endregion
    }
}
