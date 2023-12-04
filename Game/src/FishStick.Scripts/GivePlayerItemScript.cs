using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Scripts
{
  class GivePlayerItemScript : IDialogueScript
  {
    private IItem _item;

    public void Execute(PlayerController player, WorldController world)
    {
      player.TakeItem(_item);
      ConsoleController.WriteText($"You received {_item.Name}.");
    }

    public GivePlayerItemScript(IItem item)
    {
      _item = item;
    }
  }
}
