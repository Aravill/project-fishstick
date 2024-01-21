using FishStick.Scripts;

namespace Dialogue
{
  public class DialogueData
  {
    public int FirstLineIndex { get; set; } = 0;
    public bool WasHad { get; set; }
    public bool Repeatable { get; set; }

    public ScriptList? Scripts { get; set; }

    public IDialogueCondition? Condition { get; set; }

    public List<int[]> UsedReplies { get; } = new();

    public void UseReply(int lineIndex, int replyIndex)
    {
      UsedReplies.Add(new[] { lineIndex, replyIndex });
    }

    public bool WasReplyUsed(int lineIndex, int replyIndex)
    {
      return UsedReplies.Find(x => x[0] == lineIndex && x[1] == replyIndex) != null;
    }
  }

  public class ScriptList : List<(int[]?, IDialogueScript)>
  {
    public List<IDialogueScript> GetReplyScripts(int lineIndex, int replyIndex)
    {
      List<IDialogueScript> scripts = new();
      foreach (var script in this)
      {
        // Item1 is [lineIndex, replyIndex]
        if (script.Item1 == null)
        {
          continue;
        }
        if (script.Item1[0] == lineIndex && script.Item1[1] == replyIndex)
        {
          scripts.Add(script.Item2);
        }
      }
      return scripts;
    }
  }
}