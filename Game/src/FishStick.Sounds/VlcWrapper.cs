
namespace FishStick.Sounds
{
  public class VlcWrapper : ISoundPlayerWrapper
  {
    private string _soundLocation = string.Empty;


    public string SoundLocation
    {
      set
      {
        _soundLocation = value;
      }
    }

    public void Play(bool once)
    {

    }

    public void Load()
    {

    }

    public void Stop()
    {
    }
  }
}