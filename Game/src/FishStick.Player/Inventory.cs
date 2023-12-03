using FishStick.Item;

namespace FishStick.Player
{
    class Inventory
    {
        private List<IItem> _items = new();

        public IItem? GetItem(string name)
        {
            return _items.Find(item => item.Name == name);
        }

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
    }
}
