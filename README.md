# NWaves.Samples

### NWaves.DemoForms

The simplest demo app among all samples in this repo. It's just a bunch of Win Forms (for feature extraction, transforms, filtering, etc.) and can be considered as a complementary GUI-app for ```NWaves.Tests``` with visualization of signals, spectrograms, and so on. No OOD, MVVM, etc. here whatsoever. The code's as simple as possible, so that even users not familiar with modern C#/.NET ecosystem could take a look at NWaves in action.


### NWaves.DemoStereo

This app demonstrates various [stereo audio effects](https://github.com/ar1st0crat/NWaves/wiki/Stereo-effects):

- Stereo pan effect
- ITD-ILD pan effect
- Binaural pan effect

If the ```ITD-ILD``` checkbox is checked, ```ItdIldPanEffect``` will be applied. Otherwise, if HRIRs are already loaded, binaural panning will take place. Otherwise, the basic stereo panning is applied (using the pan rule selected in the corresponding combobox).

CIPIC_RIRS folder contains data from [CIPIC database](https://www.ece.ucdavis.edu/cipic/spatial-sound/hrtf-data/) (3 randomly selected subjects for demo).

In order to specify HRIRs, simply click on any file in one of the subject's folders.

<img src="https://github.com/ar1st0crat/NWaves.Samples/blob/main/screenshots/stereo.png" width="600" />


### NWaves.DemoMfccOnline

This simple WPF app lets you load audio from any WAV file or record audio from selected input device and compute its MFCC vectors on the fly, during the playback/recording.

![onlinedemo](https://github.com/ar1st0crat/NWaves.Samples/blob/main/screenshots/mfccdemo.gif)


### NWaves.DemoUwp

This demo app shows how we can add NWaves audio effects, filters and block convolvers to UWP projects for online audio processing. In this example we work with ```AutowahEffect``` and allow user online-tweaking only couple of its parameters: maximum LFO frequency and Q factor.

<img src="https://github.com/ar1st0crat/NWaves.Samples/blob/main/screenshots/uwp.png" width="480" />

Most of the code simply repeats [AudioCreation UWP sample code](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/AudioCreation/cs).

Effect is added to the ```AudioGraph``` here:

```C#
private void AddCustomEffect()
{
    PropertySet wahwahProperties = new PropertySet
    {
        { "Max frequency", 2000f },
        { "Q", 0.5f }
    };

    AudioEffectDefinition wahwahDefinition =
        new AudioEffectDefinition(typeof(NWavesEffect).FullName, wahwahProperties);

    fileInputNode.EffectDefinitions.Add(wahwahDefinition);
}
```


According to MS documentation, custom sound effects must be implemented in separate projects as Windows runtime components. [This project contains implementation of the effect](https://github.com/ar1st0crat/NWaves.Samples/tree/main/NWaves.DemoUwpEffect).


### NWaves.DemoXamarin

This demo app shows how we can do online audio processing in Xamarin.

The app

- estimates pitch online using 2 methods (ZCR and autocorrelation)
- applies robotization effect online
- saves robotized audio to wav file on Android device

1) Launch app, 2) press "Rec" button, 3) start talking, 4) press "Stop" button, 5) check recorded file.

Robotized audio will be recorded to ```Android/media``` folder.


### More

- [NWaves.Blueprints](https://github.com/ar1st0crat/NWaves.Blueprints) (Audiograph editor (NWaves + NAudio + Caliburn.Micro))
- [NWaves.Synthesizer](https://github.com/ar1st0crat/NWaves.Synthesizer) (Guitar & organ synthesis (NWaves + NAudio + Caliburn.Micro))
- [NWaves.VoiceEffects](https://github.com/ar1st0crat/NWaves.VoiceEffects) (Morpher, Robotizer, Whisperizer (NWaves + Caliburn.Micro))
- [NWaves.Playground](https://github.com/ar1st0crat/NWaves.Playground) (NWaves + Blazor)
