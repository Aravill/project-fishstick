using Character;
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



    /// <summary>
    /// Filters out dialogues that are ineligible to be initiated. Either by being not repeatable or by having a condition that is not met.Cascadia Mono Light
    /// </summary>
    /// <param name="npc">An NPC instance</param>
    /// <returns>A list of initiatable dialogues</returns>
    private List<IDialogue> FilterDialogues(NPC npc)
    {
      List<IDialogue> dialogues = DialogueParser.ParseAllDialogues(npc.Dialogues);
      List<IDialogue> ordered = dialogues
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

    /// <summary>
    /// Executes the scripts in the replies
    /// </summary>
    /// <param name="replies"></param>
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

    /// <summary>
    /// Marks a dialogue as used
    /// </summary>
    /// <param name="dialogue"></param>
    private void UseDialogue(IDialogue dialogue)
    {
      dialogue.Use();
    }

    /// <summary>
    /// Remembers the last dialogue used by an NPC
    /// </summary>
    /// <param name="npcId"></param>
    /// <param name="dialogueId"></param>
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
