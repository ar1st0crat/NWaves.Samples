using NWaves.Operations;
using NWaves.Signals;
using NWaves.Signals.Builders;
using System;
using System.Windows.Forms;

namespace NWaves.DemoForms
{
    public partial class ModulationForm : Form
    {
        public enum ModulationMode
        {
            Amplitude,
            Frequency,
            Phase
        }

        ModulationMode _modulationMode = ModulationMode.Amplitude;
        DiscreteSignal _modulated;

        public ModulationForm()
        {
            InitializeComponent();
        }

        private void modulateButton_Click(object sender, EventArgs e)
        {
            var carrierFrequency = float.Parse(carrierFrequencyTextBox.Text);
            var carrierAmplitude = 1.0f;
            var modulationFrequency = float.Parse(modulationFrequencyTextBox.Text);
            var modulationIndex = float.Parse(modulationIndexTextBox.Text);

            var carrier = new CosineBuilder()
                                    .SetParameter("min", -carrierAmplitude)
                                    .SetParameter("max",  carrierAmplitude)
                                    .SetParameter("freq", carrierFrequency)
                                    .OfLength(1024)
                                    .SampledAt(16000)
                                    .Build();

            var baseband = new CosineBuilder()
                                    .SetParameter("min", -modulationIndex)
                                    .SetParameter("max",  modulationIndex)
                                    .SetParameter("freq", modulationFrequency)
                                    .OfLength(1024)
                                    .SampledAt(16000)
                                    .Build();

            switch (_modulationMode)
            {
                case ModulationMode.Frequency:
                    _modulated = Modulator.Frequency(baseband, carrierAmplitude, carrierFrequency);
                    break;

                case ModulationMode.Phase:
                    _modulated = Modulator.Phase(baseband, carrierAmplitude, carrierFrequency);
                    break;

                default:
                    _modulated = Modulator.Amplitude(carrier, modulationFrequency, modulationIndex);
                    break;
            }
            
            modulatedPlot.Line = _modulated.Samples;
            modulatedPlot.Markline = baseband.Samples;
        }

        private void demodulateButton_Click(object sender, EventArgs e)
        {
            DiscreteSignal demodulated;

            switch (_modulationMode)
            {
                case ModulationMode.Frequency:
                case ModulationMode.Phase:
                    demodulated = Modulator.DemodulateFrequency(_modulated);
                    break;

                default:
                    demodulated = Modulator.DemodulateAmplitude(_modulated);
                    break;
            }
            
            demodulatedPlot.Markline = demodulated.Samples;
            demodulatedPlot.Line = _modulated.Samples;
        }

        private void amplitudeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (amplitudeRadioButton.Checked) _modulationMode = ModulationMode.Amplitude;
        }

        private void frequencyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (frequencyRadioButton.Checked) _modulationMode = ModulationMode.Frequency;
        }

        private void phaseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (phaseRadioButton.Checked) _modulationMode = ModulationMode.Phase;
        }
    }
}
