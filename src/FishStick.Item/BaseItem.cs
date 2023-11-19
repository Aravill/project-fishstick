namespace FishStick.Item
{
  class BaseItem(string Id, string Name, string description, string SceneDescription, string Type, string[] Tags) : IItem
  {
    string IItem.Name { get; } = Name;
    string IItem.Description { get; } = description;
    string IItem.SceneDescription { get; } = SceneDescription;
    string IItem.Type { get; } = Type;
    string IItem.Id { get; } = Id;
    string[] IItem.Tags { get; } = Tags;
  }
}