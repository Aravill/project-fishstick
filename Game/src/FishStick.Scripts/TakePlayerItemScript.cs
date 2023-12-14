using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.World;

namespace FishStick.Scripts
{
  class TakePlayerItemScript : IDialogueScript
  {
    private string _itemId;

    public void Execute(PlayerController player, WorldController world)
    {
      IItem? removed = player.RemoveItem(_itemId);
      if (removed != null)
      {
        string text = $"{removed.Name} was from your inventory.";
        text = char.ToUpper(text[0]) + text.Substring(1);
        ConsoleController.WriteText(text);
      }
    }

    public TakePlayerItemScript(string itemId)
    {
      _itemId = itemId;
    }
  }
}
