namespace FishStick.Combat.Narration.Enums
{
  public sealed class SoundEnum
  {

    private static readonly string _soundBasePath = "assets/sounds";
    public static readonly SoundEnum Silent = new SoundEnum("misc", "silent");
    public static readonly SoundEnum AmbienceForest = new SoundEnum("ambience", "ambience-forest");

    private SoundEnum(string category, string name)
    {
      Value = $"{_soundBasePath}/{category}/{name}.wav";
    }

    public string Value { get; private set; }
  }
}
