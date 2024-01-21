using Character;
using Dialogue;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
  class TalkCommand(PlayerController player, WorldController world, DialogueController dialogues)
    : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "talk to";

    void ICommand.Execute(string[] args)
    {
      // TODO: skipping the first arg here as it is always the word "to", figure out a better way to do this
      string npcName = string.Join(" ", args[1..]).Trim();
      if (npcName == "")
      {
        ConsoleController.WriteText("Talk to who?");
        return;
      }
      IScene? currentScene = _world.GetPlayerCurrentScene(_player);
      if (currentScene == null)
      {
        throw new Exception("Player is not in any known scene.");
      }
      NPC? npc = currentScene.GetNPCByName(npcName);
      if (npc == null)
      {
        ConsoleController.WriteText($"There is no {npcName} here.");
        return;
      }
      if (npc.Dialogues == null || npc.Dialogues.Count == 0)
      {
        ConsoleController.WriteText($"{npc.Name} has nothing to say.");
        return;
      }
      dialogues.InitiateDialogue(npc);
    }
  }
}
