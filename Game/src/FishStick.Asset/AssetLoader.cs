using FishStick.AssetData;
using FishStick.Item;
using FishStick.Scene;
using Scene;
using System.Text.RegularExpressions;

namespace FishStick.Assets
{
  public class Assets(List<SceneData> sceneData, List<ExitData> exitData, List<ItemData> itemData, List<ElementData> elementData, Dictionary<string, List<string>> containerContents)
  {
    public List<SceneData> SceneData { get; set; } = sceneData;
    public List<ExitData> ExitData { get; set; } = exitData;
    public List<ItemData> ItemData { get; set; } = itemData;
    public List<ElementData> ElementData { get; set; } = elementData;
    public Dictionary<string, List<string>> ContainerContents { get; set; } = containerContents;

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
        List<IItem> toRemove = new();
        relatedItems.ForEach(item =>
        {
          if (item is not IContainer container)
            return;

          assets.ContainerContents.TryGetValue(container.Id, out List<string>? contents);
          if (contents == null)
            return;

          // We need to find the items that are in the container and add them to the container's contents
          container.Contents = relatedItems.FindAll(item => contents.Contains(item.Id));
          // We also need to remove them from the list of items in the scene
          toRemove.AddRange(container.Contents);
        });
        relatedItems.RemoveAll(toRemove.Contains);
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
    // public static IItem AsItem(this ItemData item) => new BaseItem(item.Id, item.Name, item.Description, item.SceneDescription, item.Type, item.Hidden);

    public static IItem AsItem(this ItemData item) => item switch
    {
      ContainerItemData cid => new ContainerItem(cid.Id, cid.Name, cid.Description, cid.SceneDescription, cid.Type, cid.Hidden, cid.Locked, new List<IItem>()),
      KeyItemData kid => new KeyItem(kid.Id, kid.Name, kid.Description, kid.SceneDescription, kid.Type, kid.Hidden, kid.UnlocksContainer),
      ItemData id => new BaseItem(id.Id, id.Name, id.Description, id.SceneDescription, id.Type, id.Hidden),
      _ => throw new System.Exception("Unknown item type"),
    };
    /// <summary>
    /// Converts a list of ItemData to a list of IElement
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public static IEnumerable<IElement> AsElements(this IEnumerable<ElementData> elementData) => elementData.Select(AsElement);

    public static IElement AsElement(this ElementData eData) => eData switch
    {
      InteractableElementData ied => new InteractableElement(ied.Id, ied.Command, ied.Name, ied.OnInteract, ied.Args, ied.SceneDescription, ied.Hidden),
      ElementData => new StaticElement(eData.Id, eData.SceneDescription, eData.Hidden),
      _ => throw new System.Exception("Unknown element type"),
    };

    public static Assets ReadAssetsFromFiles()
    {
      List<SceneData> sceneData = new();
      List<ExitData> exitData = new();
      List<ItemData> itemData = new();
      List<ElementData> elementData = new();
      Dictionary<string, List<string>> containerContents = new();
      using var sceneReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/scenes.csv");
      {
        // Skip the first line
        sceneReader.ReadLine();
        while (!sceneReader.EndOfStream)
        {
          if (sceneReader.ReadCSV() is not { Length: 3 } values)
            continue;

          // For scene data: id, name, description
          string id = values[0];
          string name = values[1];
          string description = values[2];
          sceneData.Add(new SceneData(id, description, name));
        }
      }
      using var exitsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/exits.csv");
      {
        // Skip the first line
        exitsReader.ReadLine();
        while (!exitsReader.EndOfStream)
        {
          if (exitsReader.ReadCSV() is not { Length: 3 } values)
            continue;

          // For exits data: from, to, name, description
          string from = values[0];
          string to = values[1];
          string description = values[2];
          string name = description.GetName();
          exitData.Add(new ExitData(name, from, to, description));
        }
      }
      using var itemsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/items.csv");
      {
        // Skip the first line
        itemsReader.ReadLine();
        while (!itemsReader.EndOfStream)
        {
          if (itemsReader.ReadCSV() is not { Length: 8 } values) 
            continue;

          // For scene data: id, name, description
          string id = values[0];
          string containerId = values[1];
          string sceneDescription = values[2];
          string name = sceneDescription.GetName();
          string description = values[3];
          string type = values[4];
          string inScene = values[5];
          _ = bool.TryParse(values[6], out var hidden); // when parsing fails hidden = false
          _ = bool.TryParse(values[7], out var locked); // when parsing fails hidden = false
          string unlocksContainer = values[7];

          itemData.Add(type switch
          {
            "container" => new ContainerItemData(id, name, description, sceneDescription, type, inScene, hidden, locked),
            "key" => new KeyItemData(id, name, description, sceneDescription, type, inScene, hidden, unlocksContainer),
            _ => new ItemData(id, name, description, sceneDescription, type, inScene, hidden)
          });

          // Adding references of items under containers
          if (containerId == "" || containerId == null)
            continue;

          if (!containerContents.ContainsKey(containerId))
            containerContents.Add(containerId, []);

          containerContents[containerId].Add(id);
        }
      }
      using var elementsReader = new StreamReader(Directory.GetCurrentDirectory() + "/assets/elements.csv");
      {
        // Skip the first line
        elementsReader.ReadLine();
        while (!elementsReader.EndOfStream)
        {
          if (elementsReader.ReadCSV() is not { Length: >=8 } values)
            continue;

          // for element data: id,	in scene,	scene description,	hidden,	type,	name,	on interaction,	...arg 1	arg 2	arg 3	arg 4
          string id = values[0];
          string inScene = values[1];
          string sceneDescription = values[2];
          _ = bool.TryParse(values[3], out var hidden); // when parsing fails hidden = false
          string type = values[4];
          string command = values[5];
          string name = sceneDescription.GetName();
          string onInteract = values[6];
          string[] args = values[7..];

          elementData.Add(type switch
          {
            "interactable" => new InteractableElementData(id, sceneDescription, hidden, type, inScene, command, name, onInteract, args),
            "static" => new ElementData(id, sceneDescription, hidden, type, inScene),
            _ => throw new ArgumentException($"Unknown element type '{type}'")
          });
        }
      }
      return new Assets(sceneData, exitData, itemData, elementData, containerContents);
    }
    private static string[]? ReadCSV(this StreamReader reader, char separator = ',') => reader.ReadLine()?.Split(separator);
    private static Match FindTagged(this string description) => Regex.Match(description, @"\{([\w ]+)\}", RegexOptions.IgnoreCase);
    private static string GetMatch(this Match match, int group = 1) => match.Groups[group].Value;
    private static string GetName(this string description) => description.FindTagged().GetMatch();
  }
}
