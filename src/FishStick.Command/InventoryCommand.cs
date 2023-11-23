using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Commands
{
  class InventoryCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "inventory";
    void ICommand.Execute(string[] args)
    {
      string items = String.Join(", ", _player.GetInventory().Select(item => item.Name).ToArray());
      if (items.Length < 1)
      {
        ConsoleController.WriteText("I look into my bag. It is empty.");
        return;
      }
      ConsoleController.WriteText($"I look into my bag and see: {items}.");
      return;
    }
  }
}
