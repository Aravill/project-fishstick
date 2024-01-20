using FishStick;

namespace Dialogue
{
  public static class DialogueParser
  {
    public static void ParseAllDialogues(List<string> dialogueIds)
    {
      List<BaseDialogue> dialogues = new();
      for (int i = 0; i < dialogueIds.Count; i++)
      {
        dialogues.Add(ParseDialogue(dialogueIds[i], i));
      }
    }

    private static BaseDialogue ParseDialogue(string dialogueId, int order)
    {

      if (!Global.Dialogues.ContainsKey(dialogueId) || !Global.DialogueData.ContainsKey(dialogueId))
      {
        throw new Exception($"Dialogue {dialogueId} (or its metadata) does not exist");
      }
      DialogueData metadata = Global.DialogueData[dialogueId];
      List<IDialogueLine> lines = ParseAllLines(dialogueId);
      return new BaseDialogue(dialogueId, lines, LineId(dialogueId, metadata.FirstLineIndex), order, metadata.Repeatable, metadata.Condition, metadata.WasHad);
    }

    private static List<IDialogueLine> ParseAllLines(string dialogueId)
    {
      var lines = Global.Dialogues[dialogueId];
      List<IDialogueLine> parsedLines = new();
      if (lines.Count == 0)
      {
        throw new Exception($"Dialogue {dialogueId} is empty");
      }
      for (int i = 0; i < lines.Count; i++)
      {
        parsedLines.Add(ParseSingleLine(dialogueId, i));
      }
      return parsedLines;
    }

    private static DialogueLine ParseSingleLine(string dialogueId, int lineIndex)
    {
      // This is the set of a line + its replies
      List<(string, int?, bool?)> set;
      try
      {
        set = Global.Dialogues[dialogueId][lineIndex];
      }
      catch (Exception)
      {
        throw new Exception($"Dialogue {dialogueId} does not have a line with index {lineIndex}");
      }
      if (set.Count == 0)
      {
        throw new Exception($"Dialogue {dialogueId}:{lineIndex} is empty");
      }
      // The first line is always the NPC line, the rest are replies
      DialogueLine line = BuildLine(dialogueId, lineIndex, set[0]);
      line.Replies = new();
      // Starting at 1 to skip the NPC line and only iterate over replies
      for (int j = 1; j < set.Count; j++)
      {
        (string, int?, bool?) lineData = set[j];
        // The rest are replies
        line.Replies.Add(
          BuildReply(dialogueId, lineIndex, lineData)
        );
      }
      return line;
    }

    private static DialogueLine BuildLine(string dialogueId, int lineIndex, (string, int?, bool?) lineData)
    {
      // Item1 is the text
      // Item2 is the next line index
      // Item3 is whether or not the next line should be read
      return new DialogueLine(
        LineId(dialogueId, lineIndex),
        lineData.Item1,
        null,
        lineData.Item3,
        lineData.Item2 != null ? LineId(dialogueId, lineData.Item2.Value) : null
        );
    }

    private static Reply BuildReply(string dialogueId, int lineIndex, (string, int?, bool?) lineData)
    {
      if (lineData.Item2 == null)
      {
        throw new Exception($"Reply {dialogueId}:{lineIndex} does not have a next line index");
      }
      return new Reply(
        lineData.Item1,
        LineId(dialogueId, lineData.Item2.Value),
        lineData.Item3 != null ? lineData.Item3.Value : true
      );
    }

    private static string LineId(string dialogueId, int lineIndex)
    {
      // The ID is built as "dialogue-id:line-index"
      return $"{dialogueId}:{lineIndex}";
    }
  }
}