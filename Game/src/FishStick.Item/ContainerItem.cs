using System.Runtime.Serialization;

namespace FishStick.Item
{
  [DataContract]
  class ContainerItem : BaseItem, IContainer
  {
    bool IContainer.Locked
    {
      get => _locked;
    }
    List<IItem> IContainer.Contents
    {
      get => _contents;
      set => _contents = value;
    }

    [DataMember]
    private bool _locked;
    [DataMember]
    private List<IItem> _contents;

    IItem? IContainer.FindItem(string itemName)
    {
      return _contents.Find(Item => Item.Name == itemName && Item.Hidden == false);
    }

    void IContainer.RemoveItem(IItem item)
    {
      _contents.Remove(item);
    }

    void IContainer.Unlock()
    {
      _locked = false;
    }

    void IContainer.Lock()
    {
      _locked = true;
    }

    public ContainerItem(
      string Id,
      string Name,
      string Description,
      string SceneDescription,
      string Type,
      bool Hidden,
      bool Locked,
      List<IItem> Contents
    )
      : base(Id, Name, Description, SceneDescription, Type, Hidden)
    {
      _contents = Contents;
      _locked = Locked;
    }
  }
}
