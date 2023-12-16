using FishStick.Assets;
using FishStick.Item;
using FishStick.Player;
using FishStick.Scene;

namespace FishStick.World
{
  public class WorldController
  {
    private Dictionary<string, IScene> _sceneMap = new();

    public WorldController()
    {
      List<IScene> scenes = AssetLoader.Load();
      scenes.ForEach(scene =>
      {
        _sceneMap.Add(scene.Id, scene);
      });
      // TODO: Remove this later, it's just for testing
      IScene sampleScene = SampleScene.BuildSampleScenes();
      _sceneMap.Add(sampleScene.Id, sampleScene);
    }

    public IScene GetScene(string sceneId)
    {
      if (!_sceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        throw new Exception($"Scene {sceneId} not found.");
      }
      return scene;
    }

    public List<IScene> GetScenes()
    {
      return _sceneMap.Values.ToList();
    }

    public IScene? GetPlayerCurrentScene(PlayerController player)
    {
      if (!_sceneMap.TryGetValue(player.GetCurrentSceneId(), out IScene? scene))
      {
        return null;
      }
      return scene;
    }

    public IItem? GetItem(string sceneId, string itemId)
    {
      if (!_sceneMap.TryGetValue(sceneId, out IScene? scene))
      {
        return null;
      }
      return scene.GetItem(itemId);
      ;
    }
  }
}
