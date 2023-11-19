using FishStick.Commands;
using FishStick.Player;
using FishStick.World;

class CommandDictionary : Dictionary<string, ICommand>
{
  public CommandDictionary(PlayerController player, WorldController world)
  {
    // TODO: It would be really sexy if we could just add all the commands in the assembly, identified by the ICommand interface.
    Add(GoCommand.Name, new GoCommand(player, world));
    Add(QuitCommand.Name, new QuitCommand(player, world));
  }
}