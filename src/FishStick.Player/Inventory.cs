using FishStick.Item;

namespace FishStick.Player
{
  class Inventory
  {

    private List<IItem> _items = new();
    public void AddItem(IItem item)
    {
      _items.Add(item);
    }

    public void RemoveItem(IItem item)
    {
      _items.Remove(item);
    }

    public List<IItem> GetItems()
    {
      return _items;
    }

    public List<IItem> GetItemsByTag(string tag)
    {
      return _items.Where(item => item.Tags.Contains(tag)).ToList();
    }
  }
}
