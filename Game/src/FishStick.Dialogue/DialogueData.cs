using FishStick.Scripts;

namespace Dialogue
{
  public class DialogueData
  {
    public int FirstLineIndex { get; set; } = 0;
    public bool WasHad { get; set; }
    public bool Repeatable { get; set; }

    public List<(int[]?, IDialogueScript)>? Scripts { get; set; }

    public IDialogueCondition? Condition { get; set; }
  }
}