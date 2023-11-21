using FishStick.Exception;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scripts;
using FishStick.World;
using Scene;

namespace FishStick.Commands
{
  class CommandController(PlayerController player, WorldController world)
  {
    private CommandDictionary _commands = new(player, world);
    private ScriptController _scripts = new();
    public void Execute(string command)
    {
      string[] args = command.Split(" ");
      string commandName = args[0];
      args = args[1..].Select(arg => arg.Trim()).ToArray();
      if (!_commands.ContainsKey(commandName))
      {
        // Try to find interactable element with this command name
        IInteractable? element = world.GetScene(player.GetCurrentSceneId()).GetElementByCommandName(commandName);
        if (element == null || element.Hidden)
        {
          ConsoleController.WriteText($"I don't know how to '{commandName}'.");
          return;
        }
        _scripts.ExecuteScript(player, world, element.OnInteract, args);
        return;
      }
      _commands[commandName].Execute(args);
    }
  }
}