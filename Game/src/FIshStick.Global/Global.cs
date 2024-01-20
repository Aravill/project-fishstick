using System.Net;
using Dialogue;
using FishStick.Scripts;

namespace FishStick
{
  public static class Global
  {
    public static Dictionary<string, List<List<(string, int?, bool?)>>> Dialogues { get; } = new();

    public static Dictionary<string, DialogueData> DialogueData { get; } = new();

  }
}