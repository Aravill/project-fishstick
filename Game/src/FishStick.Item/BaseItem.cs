using Scene;

namespace FishStick.Item
{
  class BaseItem(
    string Id,
    string Name,
    string description,
    string SceneDescription,
    string Type,
    bool Hidden
  ) : IItem
  {
    string IItem.Name { get; } = Name;
    string IItem.Description { get; } = description;
    string ISceneDescribable.SceneDescription { get; } = SceneDescription;
    string IItem.Type { get; } = Type;
    string IItem.Id { get; } = Id;
    bool IHiddeable.Hidden { get; set; } = Hidden;
  }
}
