using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;
using Scene;

namespace FishStick.Scripts
{
  class RevealScript : IScript
  {
    public void Execute(PlayerController player, WorldController world, string[] args)
    {
      string sceneId = args[0];
      string id = args[1];
      string type = args[2];
      IScene? scene = world.GetScene(sceneId);
      switch (type)
      {
        case "item":
          IItem? item = scene.GetItem(id);
          if (item != null)
          {
            item.Hidden = false;
            ConsoleController.WriteText(item.SceneDescription);
          }
          break;
        case "element":
          IElement? element = scene.GetElement(id);
          if (element != null)
          {
            element.Hidden = false;
            ConsoleController.WriteText(element.SceneDescription);
          }
          break;
        default:
          throw new Exception($"Trying to reveal an unknown object type {type}.");
      }
    }
  }
}
