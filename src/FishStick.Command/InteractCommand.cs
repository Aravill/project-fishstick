
using FishStick.Player;
using FishStick.Render;
using FishStick.Scripts;
using FishStick.World;
using Scene;

namespace FishStick.Commands
{
  class InteractCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "interact";

    private ScriptController _scripts = new();
    void ICommand.Execute(string[] args)
    {
      // Uniquely, a part of this command's args is the command name itself.
      string commandName = args[0];
      args = args[1..];
      // An attempt at a multi worded target name resolution
      string potentialTargetName;
      // First try each word separately
      for (int i = 0; i < args.Length; i++)
      {
        potentialTargetName = args[i];
        if (!AttemptInteraction(commandName, potentialTargetName)) continue;
      }
      // Then try to join them together
      potentialTargetName = string.Join(" ", args);
      // TODO: this is stupid, so i commented it out, but i'm leaving it here for posterity
      // for (int i = 0; i < args.Length; i++)
      // {
      //   potentialTargetName = string.Join(" ", args[0..(i + 1)]);
      //   bool result = AttemptInteraction(commandName, potentialTargetName);
      //   if (!result) continue;
      //   return;
      // }
      if (AttemptInteraction(commandName, potentialTargetName)) return;
      if (potentialTargetName.Length < 1)
      {
        ConsoleController.WriteText($"I don't know how to '{commandName}'");
        return;
      }
      ConsoleController.WriteText($"I don't know how to '{commandName}' a '{potentialTargetName}'.");
    }

    private bool AttemptInteraction(string commandName, string potentialTargetName)
    {
      IInteractable? element = _world.GetScene(_player.GetCurrentSceneId()).GetElementByName(potentialTargetName);
      if (element == null || element.Hidden)
      {
        // Command failure5
        return false;
      }
      if (commandName != element?.Command)
      {
        // We hit the target but the command is incorrect
        ConsoleController.WriteText($"I cannot {commandName} the {potentialTargetName}.");
        return true;
      }
      ConsoleController.WriteText($"I {commandName} the {potentialTargetName}.");
      _scripts.ExecuteScript(_player, _world, element.OnInteract, element.Args);
      // Command success
      return true;
    }


  }
}
