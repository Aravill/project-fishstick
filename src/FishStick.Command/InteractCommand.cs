
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
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
      string potentialTargetName = "";
      // First try each word separately
      for (int i = 0; i < args.Length; i++)
      {
        potentialTargetName = args[i];
        bool result = AttemptInteraction(commandName, potentialTargetName);
        if (!result) continue;
        return;
      }
      // Then try to join them together
      for (int i = 0; i < args.Length; i++)
      {
        potentialTargetName = string.Join(" ", args[0..(i + 1)]);
        bool result = AttemptInteraction(commandName, potentialTargetName);
        if (!result) continue;
        return;
      }
      ConsoleController.WriteText($"I don't know how to '{commandName}' a '{potentialTargetName}'.");
    }

    private bool AttemptInteraction(string commandName, string potentialTargetName)
    {
      IInteractable? element = _world.GetScene(_player.GetCurrentSceneId()).GetElementByTarget(potentialTargetName);
      if (element == null || element.Hidden)
      {
        // Command failure
        return false;
      }
      ConsoleController.WriteText($"You {commandName} the {potentialTargetName}.");
      _scripts.ExecuteScript(_player, _world, element.OnInteract, element.Args);
      // Command success
      return true;
    }


  }
}
