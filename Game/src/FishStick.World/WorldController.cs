using FishStick.Assets;
using FishStick.Item;
using FishStick.Player;
using FishStick.Scene;

namespace FishStick.World
{
  public class WorldController
  {
    private Dictionary<string, IScene> SceneMap = new();

    public WorldController()
    {
      List<IScene> scenes = AssetLoader.Load();
      scenes.ForEach(scene =>
      {
        SceneMap.Add(scene.Id, scene);
      });
      // TODO: Remove this later, it's just for testing
      IScene sampleScene = SampleScene.BuildSampleScenes();
      SceneMap.Add(sampleScene.Id, sampleScene);
    }

    public IScene GetScene(string sceneId)
    {
      if (!SceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        throw new Exception($"Scene {sceneId} not found.");
      }
      return scene;
    }

    public IScene? GetPlayerCurrentScene(PlayerController player)
    {
      if (!SceneMap.TryGetValue(player.GetCurrentSceneId(), out IScene? scene))
      {
        return null;
      }
      return scene;
    }

    public IItem? GetItem(string sceneId, string itemId)
    {
      if (!SceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        return null;
      }
      return scene.GetItem(itemId);
      ;
    }
  }
}
