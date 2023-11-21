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
      string elementId = args[1];
      IScene? scene = world.GetScene(sceneId);
      IElement? element = scene.GetElement(elementId);
      element.Hidden = false;
      ConsoleController.WriteText(element.SceneDescription);
    }
  }
}