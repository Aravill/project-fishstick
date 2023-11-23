namespace FishStick.Item
{
  public interface IItem
  {

    // Returns the item's name
    string Name { get; }

    // Returns the item's description
    string Description { get; }

    // Returns the item's scene description
    string SceneDescription { get; }

    // Returns the item's type
    string Type { get; }

    // Returns the item's id
    string Id { get; }
    string[] Tags { get; }
    bool Hidden { get; set; }
  }
}