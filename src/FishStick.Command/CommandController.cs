using FishStick.Exception;
using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Commands
{
  class CommandController(PlayerController player, WorldController world)
  {
    private CommandDictionary _commands = new(player, world);

    public void Execute(string command)
    {
      string[] args = command.Split(" ");
      string commandName = args[0];
      args = args[1..].Select(arg => arg.Trim()).ToArray();
      if (!_commands.ContainsKey(commandName))
      {
        ConsoleController.WriteText($"I don't know how to '{commandName}'.");
        return;
      }
      _commands[commandName].Execute(args);
    }
  }
}