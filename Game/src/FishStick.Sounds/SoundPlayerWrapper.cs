namespace FishStick.Sounds
{
  public interface ISoundPlayerWrapper
  {
    string SoundLocation { set; }
    void Play();
    void Load();
    void Stop();
  }
}