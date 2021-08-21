﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NWaves.Audio;
using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Base;
using NWaves.FeatureExtractors.Multi;
using NWaves.FeatureExtractors.Options;
using NWaves.Signals;
using NWaves.Features;
using System.Drawing;
using NWaves.Transforms;

namespace NWaves.DemoForms
{
    public partial class FeaturesForm : Form
    {
        private DiscreteSignal _signal;
        private float[][] _vectors;

        private int _frameSize = 512;
        private int _hopSize = 128;

        private Stft _stft;

        public FeaturesForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (var stream = new FileStream(ofd.FileName, FileMode.Open))
            {
                var waveFile = new WaveFile(stream);
                _signal = waveFile[Channels.Left];
            }

            _stft = new Stft(_frameSize, _hopSize);

            var frameDuration = (double) _frameSize / _signal.SamplingRate;
            var hopDuration = (double) _hopSize / _signal.SamplingRate;

            var freqs = new[] { 300f, 600, 1000, 2000, 4000, 7000 };

            var pitchOptions = new PitchOptions
            {
                SamplingRate = _signal.SamplingRate,
                FrameDuration = frameDuration,
                HopDuration = hopDuration,
                HighFrequency = 900/*Hz*/
            };

            var pitchExtractor = new PitchExtractor(pitchOptions);
            var pitchTrack = pitchExtractor.ParallelComputeFrom(_signal)
                                           .Select(p => p[0])
                                           .ToArray();

            var options = new MultiFeatureOptions
            {
                SamplingRate = _signal.SamplingRate,
                FrameDuration = frameDuration,
                HopDuration = hopDuration
            };

            var tdExtractor = new TimeDomainFeaturesExtractor(options);
            tdExtractor.AddFeature("pitch_zcr", (signal, start, end) => Pitch.FromZeroCrossingsSchmitt(signal, start, end));

            var mpeg7Extractor = new Mpeg7SpectralFeaturesExtractor(options);
            mpeg7Extractor.IncludeHarmonicFeatures("all");
            mpeg7Extractor.SetPitchTrack(pitchTrack);

            options.FeatureList = "sc+sn";
            options.Frequencies = freqs;
            var spectralExtractor = new SpectralFeaturesExtractor(options);
            //spectralExtractor.AddFeature("pitch_hss", (spectrum, fs) => Pitch.FromHss(spectrum, _signal.SamplingRate));

            var tdVectors = tdExtractor.ParallelComputeFrom(_signal);
            var spectralVectors = spectralExtractor.ParallelComputeFrom(_signal);
            var mpeg7Vectors = mpeg7Extractor.ComputeFrom(_signal);

            _vectors = FeaturePostProcessing.Join(tdVectors, spectralVectors, mpeg7Vectors);

            //FeaturePostProcessing.NormalizeMean(_vectors);
            //FeaturePostProcessing.AddDeltas(_vectors);

            var descriptions = tdExtractor.FeatureDescriptions
                                          .Concat(spectralExtractor.FeatureDescriptions)
                                          .Concat(mpeg7Extractor.FeatureDescriptions)
                                          .ToList();

            FillFeaturesList(_vectors, descriptions, tdExtractor.TimeMarkers(_vectors.Length));

            spectrogramPlot.ColorMapName = "afmhot";
            spectrogramPlot.MarklineThickness = 2;
            spectrogramPlot.Spectrogram = _stft.Spectrogram(_signal);
        }

        private void FillFeaturesList(IList<float[]> featureVectors,
                                      IList<string> featureDescriptions,
                                      IList<double> timeMarkers)
        {
            featuresListView.Clear();
            featuresListView.Columns.Add("time", 50);

            foreach (var feat in featureDescriptions)
            {
                featuresListView.Columns.Add(feat, 70);
            }

            for (var i = 0; i < featureVectors.Count; i++)
            {
                var item = new ListViewItem { Text = timeMarkers[i].ToString("F4") };
                item.SubItems.AddRange(featureVectors[i].Select(f => f.ToString("F4")).ToArray());

                featuresListView.Items.Add(item);
            }
        }

        private void featuresListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                return;
            }

            featureLabel.Text = featuresListView.Columns[e.Column].Text;

            var max = _vectors.Select(v => v[e.Column - 1]).Max();
            var min = _vectors.Select(v => v[e.Column - 1]).Min();

            var height = spectrogramPlot.Height;

            spectrogramPlot.Markline = _vectors.Select(v =>  height * (v[e.Column - 1] - min) / (max - min)).ToArray();

            //featurePlotPanel.Stride = 1;
            //featurePlotPanel.Line = _vectors.Select(v => v.Features[e.Column - 1]).ToArray();
        }


        // TODO: remove this )))

        private void featuresListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (featuresListView.SelectedItems.Count == 0)
            {
                return;
            }

            var pos = featuresListView.SelectedIndices[0];

            var fft = new Fft(512);

            var spectrum = fft.PowerSpectrum(_signal[pos * _hopSize, pos * _hopSize + _frameSize]).Samples;

            var peaks = new int[10];
            var freqs = new float[10];


            Harmonic.Peaks(spectrum, peaks, freqs, _signal.SamplingRate);


            peaksListBox.Items.Clear();
            for (var p = 0; p < peaks.Length; p++)
            {
                peaksListBox.Items.Add($"peak #{p+1,-2} : {freqs[p],-7} Hz");
            }


            _spectrumImage = new Bitmap(512, spectrumPictureBox.Height);

            var g = Graphics.FromImage(_spectrumImage);
            g.Clear(Color.White);

            var pen = new Pen(ForeColor);
            var redpen = new Pen(Color.Red, 2);

            var i = 1;
            var Stride = 4;
            var PaddingX = 5;
            var PaddingY = 5;

            var x = PaddingX + Stride;

            var min = spectrum.Min();
            var max = spectrum.Max();

            var height = _spectrumImage.Height;
            var gain = max - min < 1e-6 ? 1 : (height - 2 * PaddingY) / (max - min);

            var offset = (int)(height - PaddingY + min * gain);

            for (; i < spectrum.Length; i++)
            {
                g.DrawLine(pen, x - Stride, -spectrum[i - 1] * gain + offset,
                                x,          -spectrum[i    ] * gain + offset);
                x += Stride;
            }

            for (i = 0; i < peaks.Length; i++)
            {
                g.DrawLine(redpen, PaddingX + peaks[i] * Stride,  PaddingY + offset,
                                   PaddingX + peaks[i] * Stride, -PaddingY - spectrum[peaks[i]] * gain + offset);
            }

            pen.Dispose();
            redpen.Dispose();
            g.Dispose();

            spectrumPictureBox.Image = _spectrumImage;
        }

        Bitmap _spectrumImage;
    }
}
