using Character;
using FishStick;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scripts;
using FishStick.World;

namespace Dialogue
{
  class DialogueController(WorldController world, PlayerController player)
  {
    private WorldController _world = world;
    private PlayerController _player = player;

    private Dictionary<string, string> _dialogueDictionary = new Dictionary<string, string>();

    public void InitiateDialogue(NPC npc)
    {
      IDialogue? nextDialogue = GetNextDialogue(npc);
      if (nextDialogue == null) // No dialogues we can initiate
      {
        ConsoleController.WriteText($"{npc.Name} has nothing to say.");
        return;
      }
      List<IReply> chosenReplies = DialogueStrategy.HandleDialogue(nextDialogue);
      ProcessReplies(chosenReplies);
      UseDialogue(nextDialogue);
      RememberDialogue(npc.Id, nextDialogue.Id);
    }

    private IDialogue? GetNextDialogue(NPC npc)
    {
      string? lastDialogueId = _dialogueDictionary.ContainsKey(npc.Id)
        ? _dialogueDictionary[npc.Id]
        : null;
      // Select dialogues we can initiate
      List<IDialogue> possibleDialogues = FilterDialogues(npc);
      if (possibleDialogues.Count == 0)
        return null;
      if (possibleDialogues[0].WasHad == false)
      {
        // If the first dialogue was not had, return it
        return possibleDialogues[0];
      }
      int lastUsedIndex =
        lastDialogueId != null
          ? possibleDialogues.FindIndex(dialogue => dialogue.Id == lastDialogueId)
          : 0;
      if (lastUsedIndex == -1 || lastUsedIndex == possibleDialogues.Count - 1)
      {
        // If the last used dialogue is not in the list or is the last one in the list, return the first one
        lastUsedIndex = 0;
      }
      // Return the next dialogue in the list
      return possibleDialogues[lastUsedIndex + 1];
    }

    private void RetrieveDialogues(List<string> dialogueIds)
    {
      List<BaseDialogue> dialogues = new();
      for (int i = 0; i < dialogueIds.Count; i++)
      {
        dialogues.Add(ParseDialogue(dialogueIds[i], i));
      }
    }

    private BaseDialogue ParseDialogue(string dialogueId, int order)
    {

      if (!Global.Dialogues.ContainsKey(dialogueId) || !Global.DialogueData.ContainsKey(dialogueId))
      {
        throw new Exception($"Dialogue {dialogueId} (or its metadata) does not exist");
      }
      DialogueData metadata = Global.DialogueData[dialogueId];
      List<IDialogueLine> lines = ParseAllLines(dialogueId);
      return new BaseDialogue(dialogueId, lines, LineId(dialogueId, metadata.FirstLineIndex), order, metadata.Repeatable, metadata.Condition, metadata.WasHad);
    }

    private List<IDialogueLine> ParseAllLines(string dialogueId)
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

    private DialogueLine ParseSingleLine(string dialogueId, int lineIndex)
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

    private DialogueLine BuildLine(string dialogueId, int lineIndex, (string, int?, bool?) lineData)
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

    private Reply BuildReply(string dialogueId, int lineIndex, (string, int?, bool?) lineData)
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

    private string LineId(string dialogueId, int lineIndex)
    {
      // The ID is built as "dialogue-id:line-index"
      return $"{dialogueId}:{lineIndex}";
    }

    /// <summary>
    /// Filters out dialogues that are ineligible to be initiated. Either by being not repeatable or by having a condition that is not met.Cascadia Mono Light
    /// </summary>
    /// <param name="npc">An NPC instance</param>
    /// <returns>A list of initiatable dialogues</returns>
    private List<IDialogue> FilterDialogues(NPC npc)
    {
      List<IDialogue> ordered = npc.Dialogues
        .Where(
          dialogue =>
            // Filter out dialogues that have a condition that is not met
            dialogue.Condition == null
            || dialogue.Condition.Check(_player, _world)
              &&
              // Filter out dialogues that were had and are not repeatable
              !(dialogue.WasHad == true && dialogue.Repeatable == false)
        )
        // Sort by their used status
        .OrderBy(dialogue => dialogue.Order)
        .ToList();
      // Split the list into two lists, one with dialogues that were had and one with dialogues that were not had
      List<IDialogue> notHad = ordered.Where(dialogue => dialogue.WasHad == false).ToList();
      ordered.RemoveAll(dialogue => dialogue.WasHad == false);
      // Concatenate the two lists and return them, with the not had dialogues first
      return notHad.Concat(ordered).ToList();
    }

    private void ProcessReplies(List<IReply> replies)
    {
      foreach (IReply reply in replies)
      {
        if (reply is ScriptReply)
        {
          ScriptReply scriptReply = (ScriptReply)reply;
          foreach (IDialogueScript script in scriptReply.Scripts)
          {
            script.Execute(_player, _world);
          }
        }
      }
    }

    private void UseDialogue(IDialogue dialogue)
    {
      dialogue.WasHad = true;
    }

    private void RememberDialogue(string npcId, string dialogueId)
    {
      if (_dialogueDictionary.ContainsKey(npcId))
      {
        _dialogueDictionary[npcId] = dialogueId;
      }
      else
      {
        _dialogueDictionary.Add(npcId, dialogueId);
      }
    }
  }
}
