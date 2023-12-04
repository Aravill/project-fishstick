using FishStick.Item;
using FishStick.Player;
using FishStick.Render;
using FishStick.Scene;
using FishStick.World;
using Scene;
using static System.Formats.Asn1.AsnWriter;

namespace FishStick.Scripts
{
  class RevealScript : IScript
  {
    public void Execute(PlayerController player, WorldController world, string[] args)
    {
      (string sceneId, string id, string type) = ParseArguments(args);
      
      IScene? scene = world.GetScene(sceneId);
      object? sceneObject = GetSceneObject(scene, type, id);
      
      RevealSceneObject(sceneObject);
    }

    private static (string sceneId, string id, string type) ParseArguments(string[] args)
    {
      return (sceneId: args[0], id: args[1], type: args[2]);
    }

    private static object? GetSceneObject(IScene scene, string type, string id) => type switch
    {
      "item" => scene.GetItem(id),
      "element" => scene.GetElement(id),
      _ => throw new Exception($"Trying to reveal an unknown object type {type}.")
    };

    // Evil trickery with dynamic
    private static void RevealSceneObject(object? obj)
    {
      if (obj is null)
        return;

      dynamic sceneObject = obj;
      sceneObject.Hidden = false;
      ConsoleController.WriteText(sceneObject.SceneDescription);
    }
  }
}