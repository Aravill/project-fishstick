namespace FishStick.Sounds
{
  public interface ISoundPlayerWrapper
  {
    string SoundLocation { set; }
    void Play(bool once);
    void Load();
    void Stop();
  }
}