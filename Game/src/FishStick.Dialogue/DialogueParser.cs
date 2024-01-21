using FishStick;
using FishStick.Scripts;

namespace Dialogue
{
  public static class DialogueParser
  {
    public static List<IDialogue> ParseAllDialogues(List<string> dialogueIds)
    {
      List<IDialogue> dialogues = new();
      for (int i = 0; i < dialogueIds.Count; i++)
      {
        dialogues.Add(ParseDialogue(dialogueIds[i], i));
      }
      return dialogues;
    }

    private static BaseDialogue ParseDialogue(string dialogueId, int order)
    {

      if (!Global.Dialogues.ContainsKey(dialogueId) || !Global.DialogueData.ContainsKey(dialogueId))
      {
        throw new Exception($"Dialogue {dialogueId} (or its metadata) does not exist");
      }
      DialogueData metadata = Global.DialogueData[dialogueId];
      List<IDialogueLine> lines = ParseAllLines(dialogueId);
      return new BaseDialogue(dialogueId, lines, LineId(dialogueId, metadata.FirstLineIndex), order, metadata.Repeatable, metadata.Condition);
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
      // Starting at 1 to skip the NPC line and only iterate over replies
      for (int replyIndex = 1; replyIndex < set.Count; replyIndex++)
      {
        if (line.Replies == null)
        {
          line.Replies = new();
        }
        (string, int?, bool?) lineData = set[replyIndex];
        // The rest are replies
        line.Replies.Add(
          BuildReply(dialogueId, lineIndex, replyIndex, lineData)
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

    private static IReply BuildReply(string dialogueId, int lineIndex, int replyIndex, (string, int?, bool?) lineData)
    {
      // TODO: Maybe this should be checked, but since it's private, I don't expect it to be called
      // from anywhere else
      DialogueData metadata = Global.DialogueData[dialogueId];
      if (lineData.Item2 == null)
      {
        throw new Exception($"Reply {dialogueId}:{lineIndex} does not have a next line index");
      }
      // If we find any scripts, init as a ScriptReply, otherwise init as a Reply
      List<IDialogueScript>? scripts = metadata.Scripts?.GetReplyScripts(lineIndex, replyIndex) ?? null;
      if (scripts != null && scripts.Count > 0)
      {
        return new ScriptReply(
          ReplyId(dialogueId, lineIndex, replyIndex),
          lineData.Item1,
          LineId(dialogueId, lineData.Item2.Value),
          scripts,
          lineData.Item3 != null ? lineData.Item3.Value : true
        );
      }
      return new Reply(
        ReplyId(dialogueId, lineIndex, replyIndex),
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

    private static string ReplyId(string dialogueId, int lineIndex, int replyIndex)
    {
      // The ID is built as "dialogue-id:line-index:reply-index"
      return $"{dialogueId}:{lineIndex}:{replyIndex}";
    }
  }
}