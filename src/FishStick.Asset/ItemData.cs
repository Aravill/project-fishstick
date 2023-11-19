
namespace FishStick.AssetData
{
  public class ItemData(string id, string name, string description, string sceneDescription, string type, string[] tags, string inScene)
  {
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string SceneDescription { get; set; } = sceneDescription;
    public string Description { get; set; } = description;
    public string[] Tags { get; set; } = tags;

    public string Type { get; set; } = type;
    public string InScene { get; set; } = inScene;
  }
}
