using FishStick.Item;

namespace FishStick.Player
{
  public class PlayerController
  {
    private int _health { get; set; }
    private Inventory _inventory;
    private string _currentSceneId;

    private bool _isAlive = true;

    public void SetCurrentSceneId(string sceneId)
    {
      _currentSceneId = sceneId;
    }

    public string GetCurrentSceneId()
    {
      return _currentSceneId;
    }

    public PlayerController(int health, string startingSceneId)
    {
      _currentSceneId = startingSceneId;
      _health = health;
      _inventory = new Inventory();
    }

    public void TakeItem(IItem item)
    {
      _inventory.AddItem(item);
    }

    public IItem? RemoveItem(string itemId)
    {
      return _inventory.RemoveItem(itemId);
    }

    public List<IItem> GetInventory()
    {
      return _inventory.GetItems();
    }

    public IItem? GetInventoryItemByName(string name)
    {
      return _inventory.GetItemByName(name);
    }

    public IItem? GetInventoryItem(string itemId)
    {
      return _inventory.GetItem(itemId);
    }

    public int Health { get => _health; private set => _health = value; }

    public bool IsAlive { get => _isAlive; private set => _isAlive = value; }

    public int TakeDamage(int amount)
    {
      Health -= amount;
      if (Health <= 0)
      {
        IsAlive = false;
      }
      return Health;
    }

    public int Heal(int amount)
    {
      Health += amount;
      return Health;
    }
  }
}
