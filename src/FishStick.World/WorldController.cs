using FishStick.Assets;
using FishStick.Item;
using FishStick.Scene;

namespace FishStick.World
{
  class WorldController
  {

    private Dictionary<string, IScene> SceneMap = new();

    public WorldController()
    {
      List<IScene> scenes = AssetLoader.Load();
      scenes.ForEach(scene =>
      {
        SceneMap.Add(scene.Id, scene);
      });
    }

    public IScene GetScene(string sceneId)
    {
      if (!SceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        throw new System.Exception($"Scene {sceneId} not found.");
      }
      return scene;
    }

    public IItem? GetItem(string sceneId, string itemId)
    {
      if (!SceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        return null;
      }
      return scene.GetItem(itemId); ;
    }
  }
}
