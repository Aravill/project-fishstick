using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Commands
{
  class InspectCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "inspect";
    void ICommand.Execute(string[] args)
    {
      string targetItemName;

      targetItemName = String.Join(" ", args);
      if (targetItemName.Length < 1)
      {
        ConsoleController.WriteText("Inspect  what?");
        return;
      }
      string? itemDescription = _player.GetInventoryItem(targetItemName)?.Description;
      if (itemDescription is null)
      {
        ConsoleController.WriteText($"You don't have a {targetItemName}.");
      }
      else
      {
        ConsoleController.WriteText(itemDescription);
      }
    }
  }
}
