using System.Runtime.InteropServices;
using NAudio.Wave;

namespace FishStick.Sounds
{
  public class NAudioWrapper : ISoundPlayerWrapper
  {
    private WaveOutEvent _outputDevice;
    private string _soundLocation = string.Empty;
    private AudioFileReader? _file;

    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {
      try
      {
        _outputDevice.Play();
        if (!once)
        {
          _outputDevice.PlaybackStopped += (sender, args) =>
          {
            _outputDevice.Play();
          };
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error playing sound: ${ex.Message}");
      }
    }

    public void Load()
    {
      _file = new AudioFileReader(_soundLocation);
      _outputDevice.Init(_file);
    }

    public void Stop()
    {
      _outputDevice.Stop();
    }

    public NAudioWrapper()
    {
      _outputDevice = new WaveOutEvent();
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        _outputDevice.DeviceNumber = -1;
      }
    }
  }
}