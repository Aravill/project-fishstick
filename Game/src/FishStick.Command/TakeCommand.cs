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


    /// <summary>
    /// Take an item from the current scene, or from a container in the current scene, or from a container in the player's inventory
    /// </summary>
    /// <param name="args">Args which make up the item's name</param>
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
      IItem? item = TakeItemFromScene(targetItemName, currentScene) ?? TakeItemFromSceneContainer(targetItemName, currentScene) ?? TakeItemFromInventoryContainer(targetItemName);
      if (item == null)
      {
        ConsoleController.WriteText($"You do not see a '{targetItemName}' here.");
        return;
      }
      ConsoleController.WriteText($"You take the {targetItemName}.");
    }

    /// <summary>
    /// Find an item in the current scene
    /// </summary>
    /// <param name="targetItemName">Name of the item we're searching for</param>
    /// <param name="currentScene">Scene the player is currently in</param>
    /// <returns>IItem?</returns>
    private IItem? TakeItemFromScene(string targetItemName, IScene currentScene)
    {
      IItem? item = currentScene.Items.Find(item => item.Name == targetItemName && item.Hidden == false);
      if (item != null)
      { currentScene.Items.Remove(item); _player.TakeItem(item); }
      return item;
    }

    /// <summary>
    /// Find an item in a container in the current scene
    /// </summary>
    /// <param name="targetItemName">Name of the item we're searching for</param>
    /// <param name="currentScene">Scene the player is currently in</param>
    /// <returns>IItem?</returns>
    private IItem? TakeItemFromSceneContainer(string targetItemName, IScene currentScene)
    {
      IItem? item = null;

      List<IContainer> containers = currentScene.Items.OfType<IContainer>().ToList().FindAll(container => container.Locked == false);
      foreach (IContainer container in containers)
      {
        item = container.FindItem(targetItemName);
        if (item != null)
        {
          container.RemoveItem(item);
          _player.TakeItem(item);
          break;
        }
      }
      return item;
    }

    /// <summary>
    /// Find an item in a container in the player's inventory
    /// </summary>
    /// <param name="targetItemName">Name of the item we're searching for</param>
    /// <returns>IItem?</returns>
    private IItem? TakeItemFromInventoryContainer(string targetItemName)
    {
      IItem? item = null;

      List<IContainer> containers = _player.GetInventory().OfType<IContainer>().ToList().FindAll(container => container.Locked == false);
      foreach (IContainer container in containers)
      {
        item = container.FindItem(targetItemName);
        if (item != null)
        {
          container.RemoveItem(item);
          _player.TakeItem(item);
          break;
        }
      }
      return item;
    }
  }
}
