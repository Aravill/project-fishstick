using FishStick.Player;
using FishStick.Session;
using FishStick.World;

namespace FishStick.Commands
{
  class CommandController(PlayerController player, WorldController world)
  {
    private CommandDictionary _commands = new(player, world);

    public void Execute(string input)
    {
      // Sanitize arguments
      string[] args = input.Split(" ").Select(arg => arg.Trim()).ToArray();
      ICommand? command = FindCommand(args);
      if (command == null)
      {
        // Try an interaction
        _commands[InteractCommand.Name].Execute(args);
        return;
      }
      // Exclude the command name from the args
      args = args[1..];
      command.Execute(args);
    }

    private ICommand? FindCommand(string[] input)
    {
      for (int i = 0; i < input.Length; i++)
      {
        // Progressiely join the args to form possible multi-worded commands
        ICommand? command;
        string commandName = String.Join(" ", input[0..(i + 1)]);
        _commands.TryGetValue(commandName, out command);
        if (command != null)
        {
          return command;
        }
      }
      return null;
    }
  }
}