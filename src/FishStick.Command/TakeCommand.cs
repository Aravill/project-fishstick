using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;

namespace FishStick.Commands
{
  class TakeCommand(PlayerController player, WorldController world) : ICommand
  {
    private PlayerController _player = player;
    private WorldController _world = world;
    public static string Name = "take";
    void ICommand.Execute(string[] args)
    {
      string targetItemName;

      targetItemName = String.Join(" ", args);
      if (targetItemName.Length < 1)
      {
        ConsoleController.WriteText("Take what?");
        return;
      }
      IScene currentScene = _world.GetScene(_player.GetCurrentSceneId());
      IItem? item = currentScene.Items.Find(item => item.Name == targetItemName);
      if (item == null)
      {
        ConsoleController.WriteText($"There is no '{targetItemName}' here.");
        return;
      }
      _player.TakeItem(item);
      ConsoleController.WriteText($"You take the {targetItemName}.");
      // TODO: Handle removing the item from the scene
    }
  }
}
