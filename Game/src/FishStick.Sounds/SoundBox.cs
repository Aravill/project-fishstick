namespace FishStick.Sounds
{
  public class SoundBox
  {
    private ISoundPlayerWrapper _player;

    public SoundBox(ISoundPlayerWrapper player)
    {
      _player = player;
    }
    public void Play(string soundFilePath, bool once)
    {
      _player.SoundLocation = soundFilePath;
      _player.Load();
      _player.Play(once);
    }

    public void Stop()
    {
      _player.Stop();
    }
  }
}