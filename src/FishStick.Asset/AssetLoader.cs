using FishStick.AssetData;
using FishStick.Item;
using FishStick.Scene;
using Scene;

namespace FishStick.Assets
{

  public class Assets(List<SceneData> sceneData, List<ExitData> exitData, List<ItemData> itemData, List<ElementData> elementData)
  {
    public List<SceneData> SceneData { get; set; } = sceneData;
    public List<ExitData> ExitData { get; set; } = exitData;
    public List<ItemData> ItemData { get; set; } = itemData;

    public List<ElementData> ElementData { get; set; } = elementData;

  }
  public static class AssetLoader
  {

    public static List<IScene> Load()
    {
      List<IScene> scenes = new();
      Assets assets = ReadAssetsFromFiles();
      foreach (SceneData scene in assets.SceneData)
      {
        List<IItem> relatedItems = assets.ItemData.Where(itemData => itemData.InScene == scene.Id).AsItems().ToList();
        List<ITransition> relatedExits = assets.ExitData.Where(exitData => exitData.From == scene.Id).AsTransitions().ToList();
        List<IElement> relatedElements = assets.ElementData.Where(elementData => elementData.InScene == scene.Id).AsElements().ToList();
        scenes.Add(new BaseScene(scene.Id, scene.Description, relatedExits, relatedItems, relatedElements));
      }
      return scenes;
    }


    public static IEnumerable<ITransition> AsTransitions(this IEnumerable<ExitData> exitData) => exitData.Select(AsTransition);

    public static ITransition AsTransition(this ExitData exit) => new BaseTransition(exit.Name, exit.Description, exit.To);

    /// <summary>
    /// Converts a list of ItemData to a list of IItem
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public static IEnumerable<IItem> AsItems(this IEnumerable<ItemData> itemData) => itemData.Select(AsItem);
    public static IItem AsItem(this ItemData item) => new BaseItem(item.Id, item.Name, item.Description, item.SceneDescription, item.Type, item.Tags);

    /// <summary>
    /// Converts a list of ItemData to a list of IElement
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public static IEnumerable<IElement> AsElements(this IEnumerable<ElementData> elementData) => elementData.Select(AsElement);

    public static IElement AsElement(this ElementData eData) => eData switch
    {
      InteractableElementData ied => new InteractableElement(ied.Id, ied.Name, ied.OnInteract, ied.Args, ied.SceneDescription, ied.Hidden),
      ElementData => new StaticElement(eData.Id, eData.SceneDescription, eData.Hidden),
      _ => throw new System.Exception("Unknown element type"),
    };

    public static Assets ReadAssetsFromFiles()
    {
      List<SceneData> sceneData = new();
      List<ExitData> exitData = new();
      List<ItemData> itemData = new();
      List<ElementData> elementData = new();
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
      using var elementsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/elements.csv");
      {
        // Skip the first line
        elementsReader.ReadLine();
        while (!elementsReader.EndOfStream)
        {
          string? line = elementsReader.ReadLine();
          if (line != null)
          {
            string[] values = line.Split(',');
            // for element data: id,	in scene,	scene description,	hidden,	type,	name,	on interaction,	...arg 1	arg 2	arg 3	arg 4
            string id = values[0];
            string inScene = values[1];
            string sceneDescription = values[2];
            bool hidden;
            if (values[3] == "TRUE")
            {
              hidden = true;
            }
            else
            {
              hidden = false;
            }
            string type = values[4];
            switch (type)
            {
              case "interactable":
                string name = values[5];
                string onInteract = values[6];
                string[] args = values[7..];
                elementData.Add(new InteractableElementData(id, sceneDescription, hidden, type, inScene, name, onInteract, args));
                break;
              case "static":
                elementData.Add(new ElementData(id, sceneDescription, hidden, type, inScene));
                break;
              default:
                throw new System.Exception("Unknown element type");
            }
          }
        }
      }
      return new Assets(sceneData, exitData, itemData, elementData);
    }
  }
}
