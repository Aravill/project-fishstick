using System.Runtime.InteropServices;
using OpenTK.Audio.OpenAL;

namespace FishStick.Sounds
{
  public class OpenTkWrapper : ISoundPlayerWrapper
  {
    private string _soundLocation = string.Empty;

    private byte[]? _soundData;

    private int _buffer;
    private int _source;

    private readonly int _sampleRate = 44100;
    private readonly ALFormat _format = ALFormat.Mono8;

    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {
      AL.SourcePlay(_source);
    }

    public void Load()
    {
      _buffer = AL.GenBuffer();
      _source = AL.GenSource();
      _soundData = LoadSoundData(_soundLocation);

      HandleBuffer();

      AL.Source(_source, ALSourcei.Buffer, _buffer);
      AL.Source(_source, ALSourcef.Gain, 1f);
    }

    public void Stop()
    {
    }

    private void HandleBuffer()
    {
      string defaultDeviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
      Console.WriteLine("Default device: " + defaultDeviceName);

      var deviceNames = ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier);

      Console.WriteLine("Available devices:");
      foreach (string deviceName in deviceNames)
      {
        Console.WriteLine(deviceName);
      }
      if (_soundData == null)
      {
        return;
      }
      GCHandle handle = GCHandle.Alloc(_soundData, GCHandleType.Pinned);
      try
      {
        AL.BufferData(_buffer, _format, _soundData, _sampleRate);
      }
      finally
      {
        handle.Free();
      }
    }

    private byte[] LoadSoundData(string filename)
    {
      return File.ReadAllBytes(filename);
    }
  }
}