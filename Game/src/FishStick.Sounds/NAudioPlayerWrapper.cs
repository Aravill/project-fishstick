using FishStick.Combat.Narration.Enums;
using NAudio.Wave;

namespace FishStick.Sounds
{
  public class NAudioPlayerWrapper : ISoundPlayerWrapper
  {
    private IWavePlayer _waveOutDevice;
    private string _soundLocation = SoundEnum.Silent.Value;

    AudioFileReader _reader;

    public NAudioPlayerWrapper()
    {
      _waveOutDevice = new WaveOutEvent();
      _reader = new AudioFileReader(SoundEnum.Silent.Value);
    }

    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play()
    {
      _waveOutDevice.Play();
    }

    public void Load()
    {
      _reader = new AudioFileReader(_soundLocation);
      _waveOutDevice.Init(_reader);
    }

    public void Stop()
    {
      _waveOutDevice.Stop();
    }
  }
}