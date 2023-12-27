using Dialogue;
using FishStick.Scripts;
using FishStick.SimpleDialogues;

namespace FishStick
{
  public static class Global
  {
    public static Dictionary<string, List<List<(string, int?, bool?)>>> Dialogues { get; } = new();
    public static Dictionary<string, IDialogueCondition> DialogueConditions { get; } = new();
  }
}