using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
  class OpenCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "open";
    void ICommand.Execute(string[] args)
    {
      // Can be used either on inventory items or on interactable containers
      string itemName = String.Join(" ", args);
      // Find first in inventory
      IContainer? item = _player.GetInventoryItem(itemName) as IContainer;
      if (item == null)
      {
        // Find in scene
        IScene scene = _world.GetScene(_player.GetCurrentSceneId());
        item = scene.Items.Find(item => item.Name == itemName) as IContainer;
        if (item == null)
        {
          // Item not found
          ConsoleController.WriteText($"There is no {itemName} here to open.");
          return;
        }
      }
      if (item.Hidden)
      {
        ConsoleController.WriteText($"There is no {itemName} here to open.");
        return;
      }
      if (item.Locked)
      {
        ConsoleController.WriteText($"The {itemName} is locked.");
        return;
      }
      if (item.Contents.Count == 0)
      {
        ConsoleController.WriteText($"You open the {itemName}. It is empty.");
        return;
      }
      else
      {
        string conents = $"You open the {itemName}. Inside you find:\n" + String.Join("\n", item.Contents.Select(item => $"- {item.SceneDescription}"));
        item.Contents.ForEach(item =>
        {
          // If any of the items were hidden, reveal them so they can be looted
          item.Hidden = false;
        });
        ConsoleController.WriteText(conents);
      }
      // TODO: Technically doors are also openable, but i'll figure that out another time
    }
  }
}
