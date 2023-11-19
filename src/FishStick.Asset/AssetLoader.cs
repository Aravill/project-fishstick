using FishStick.AssetData;
using FishStick.Item;
using FishStick.Scene;

namespace FishStick.Assets
{

  public class Assets(List<SceneData> sceneData, List<ExitData> exitData, List<ItemData> itemData)
  {
    public List<SceneData> SceneData { get; set; } = sceneData;
    public List<ExitData> ExitData { get; set; } = exitData;
    public List<ItemData> ItemData { get; set; } = itemData;

  }
  public static class AssetLoader
  {

    public static List<IScene> Load()
    {
      List<IScene> scenes = new();
      Assets assets = ReadAssetsFromFiles();
      foreach (SceneData scene in assets.SceneData)
      {
        List<IItem> relatedItems = LootableDataToILootable(assets.ItemData.Where(itemData => itemData.InScene == scene.Id).ToList());
        List<ITransition> relatedExits = ExitDataToITransition(assets.ExitData.Where(exitData => exitData.From == scene.Id).ToList());
        scenes.Add(new BaseScene(scene.Id, scene.Description, relatedExits, relatedItems));
      }
      return scenes;
    }

    public static List<ITransition> ExitDataToITransition(List<ExitData> exitData)
    {
      List<ITransition> transitions = new();
      foreach (ExitData exit in exitData)
      {
        transitions.Add(new BaseTransition(exit.Name, exit.Description, exit.To));
      }
      return transitions;
    }

    public static List<IItem> LootableDataToILootable(List<ItemData> itemData)
    {
      List<IItem> lootables = new();
      foreach (ItemData item in itemData)
      {
        lootables.Add(new BaseItem(item.Id, item.Name, item.Description, item.SceneDescription, item.Type, item.Tags));
      }
      return lootables;
    }
    public static Assets ReadAssetsFromFiles()
    {
      List<SceneData> sceneData = new();
      List<ExitData> exitData = new();
      List<ItemData> itemData = new();
      using var sceneReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/scenes.csv");
      {
        // Skip the first line
        sceneReader.ReadLine();
        while (!sceneReader.EndOfStream)
        {
          string? line = sceneReader.ReadLine();
          if (line != null)
          {
            string[] values = line.Split(',');
            // For scene data: id, name, description
            string id = values[0];
            string name = values[1];
            string description = values[2];
            sceneData.Add(new SceneData(id, description, name));
          }
        }
      }
      using var exitsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/exits.csv");
      {
        // Skip the first line
        exitsReader.ReadLine();
        while (!exitsReader.EndOfStream)
        {
          string? line = exitsReader.ReadLine();
          if (line != null)
          {
            string[] values = line.Split(',');
            // For exits data: from, to, name, description
            string from = values[0];
            string to = values[1];
            string name = values[2];
            string description = values[3];
            exitData.Add(new ExitData(name, from, to, description));
          }
        }
      }
      using var itemsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/items.csv");
      {
        // Skip the first line
        itemsReader.ReadLine();
        while (!itemsReader.EndOfStream)
        {
          string? line = itemsReader.ReadLine();
          if (line != null)
          {
            string[] values = line.Split(',');
            // For scene data: id, name, description
            string id = values[0];
            string name = values[1];
            string sceneDescription = values[2];
            string description = values[3];
            string[] tags = values[4].Split(' ');
            string type = values[5];
            string inScene = values[6];
            itemData.Add(new ItemData(id, name, description, sceneDescription, type, tags, inScene));
          }
        }
      }
      return new Assets(sceneData, exitData, itemData);
    }
  }
}
