using FishStick.Player;
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
        // Try an interaction
        _commands[InteractCommand.Name].Execute([commandName, .. args]);
        return;
      }
      _commands[commandName].Execute(args);
    }
  }
}