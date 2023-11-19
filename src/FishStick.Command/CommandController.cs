using FishStick.Exception;
using FishStick.Player;
using FishStick.World;

namespace FishStick.Commands
{
  class CommandController(PlayerController player, WorldController world)
  {
    // private PlayerController _player = player;
    // private WorldController _world = world;
    private CommandDictionary _commands = new(player, world);

    public void Execute(string command)
    {
      string[] args = command.Split(" ");
      string commandName = args[0];
      args = args[1..];
      if (!_commands.ContainsKey(commandName))
      {
        throw new CommandNotFoundException($"Command {commandName} not found.");
      }
      _commands[commandName].Execute(args);
    }
  }
}