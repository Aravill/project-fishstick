using System.Runtime.Serialization;

namespace FishStick.Item
{
  [DataContract]
  class KeyItem : BaseItem, IKey
  {
    string IKey.UnlocksContainer
    {
      get => _unlocksContainer;
    }
    [DataMember]
    private string _unlocksContainer;

    public KeyItem(
      string Id,
      string Name,
      string Description,
      string SceneDescription,
      string Type,
      bool Hidden,
      string UnlocksContainer
    )
      : base(Id, Name, Description, SceneDescription, Type, Hidden)
    {
      _unlocksContainer = UnlocksContainer;
    }
  }
}
