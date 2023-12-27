using System.Runtime.Serialization;
using FishStick.Item;

namespace FishStick.Player
{
  [DataContract]
  class Inventory
  {
    [DataMember]
    private List<IItem> _items = new();

    public IItem? GetItemByName(string name)
    {
      return _items.Find(item => item.Name == name);
    }

    public IItem? GetItem(string id)
    {
      return _items.Find(item => item.Id == id);
    }

    public void AddItem(IItem item)
    {
      _items.Add(item);
    }

    public IItem? RemoveItem(string itemId)
    {
      IItem? item = GetItem(itemId);
      if (item != null)
      {
        _items.Remove(item);
      }
      return item;
    }

    public List<IItem> GetItems()
    {
      return _items;
    }
  }
}
