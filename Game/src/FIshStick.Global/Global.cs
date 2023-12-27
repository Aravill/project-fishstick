using Dialogue;
using FishStick.Scripts;
using FishStick.SimpleDialogues;

namespace FishStick
{
  public static class Global
  {
    public static Dictionary<string, (string, List<(string, int, IDialogueScript?)>?)[]> Dialogues { get; } = new();
  }
}