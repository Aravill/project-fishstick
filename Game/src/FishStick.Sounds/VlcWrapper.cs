using LibVLCSharp.Shared;

namespace FishStick.Sounds
{
  public class VlcWrapper : ISoundPlayerWrapper
  {
    private string _soundLocation = string.Empty;
    private LibVLC _libVLC;
    private MediaPlayer _mediaPlayer;

    private Media? _media;
    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {
      if (_media != null)
        _mediaPlayer.Play(_media);
    }

    public void Load()
    {
      _media = new Media(_libVLC, _soundLocation);
    }

    public void Stop()
    {
      _mediaPlayer.Stop();
    }

    public VlcWrapper()
    {
      Core.Initialize();
      _libVLC = new LibVLC();
      _mediaPlayer = new MediaPlayer(_libVLC);
    }
  }
}