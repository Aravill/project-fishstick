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
        Add(TakeCommand.Name, new TakeCommand(player, world));
        Add(InventoryCommand.Name, new InventoryCommand(player, world));
        Add(InspectCommand.Name, new InspectCommand(player, world));
        Add(LookAroundCommand.Name, new LookAroundCommand(player, world));
        Add(InteractCommand.Name, new InteractCommand(player, world));
        Add(OpenCommand.Name, new OpenCommand(player, world));
        Add(UnlockCommand.Name, new UnlockCommand(player, world));
        Add(PutCommand.Name, new PutCommand(player, world));
    }
}
