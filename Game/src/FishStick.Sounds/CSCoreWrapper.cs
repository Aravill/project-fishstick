using CSCore;
using CSCore.SoundOut;
using CSCore.Codecs.WAV;
using CSCore.Streams;

namespace FishStick.Sounds
{
  public class CSCoreWrapper : ISoundPlayerWrapper
  {
    private string _soundLocation = string.Empty;
    private bool _fileChanged = false;
    private DirectSoundOut _device = new DirectSoundOut();
    private WaveFileReader? _file;

    public string SoundLocation
    {
      set
      {
        if (_soundLocation == value)
        {
          return;
        }
        _fileChanged = true;
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {
      // if (!once)
      // {
      //   _device. += (sender, args) =>
      //   {
      //     _device.Stop();
      //     _device.Init(_file);
      //     _device.Play();
      //   };
      // }
      _device.Play();
    }

    public void Load()
    {
      _device.Dispose();
      if (_fileChanged)
      {
        _file = new WaveFileReader(_soundLocation);
      }
      _device.Initialize(_file);
    }

    public void Stop()
    {
      _device.Stop();
    }
  }
}