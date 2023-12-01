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
        ConsoleController.WriteText("Inspect what?");
        return;
      }
      string? itemDescription = _player.GetInventoryItem(targetItemName)?.Description ?? _world.GetScene(_player.GetCurrentSceneId()).Items.Find(item => item.Name == targetItemName)?.Description;
      if (itemDescription is null)
      {
        ConsoleController.WriteText($"You don't have a {targetItemName}.");
        return;
      }
      else
      {
        ConsoleController.WriteText($"You inspect the {targetItemName}, it is: {itemDescription}");
      }
    }
  }
}
