using System.Runtime.Serialization;

namespace FishStick.Item
{
  [DataContract]
  class BaseItem(
    string Id,
    string Name,
    string description,
    string SceneDescription,
    string Type,
    bool Hidden
  ) : IItem
  {
    [DataMember]
    string IItem.Name { get; } = Name;
    [DataMember]
    string IItem.Description { get; } = description;
    [DataMember]
    string IItem.SceneDescription { get; } = SceneDescription;
    [DataMember]
    string IItem.Type { get; } = Type;
    [DataMember]
    string IItem.Id { get; } = Id;
    [DataMember]
    bool IItem.Hidden { get; set; } = Hidden;
  }
}
