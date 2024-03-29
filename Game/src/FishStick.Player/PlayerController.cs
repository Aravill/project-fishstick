using FishStick.Dice;
using FishStick.Item;

namespace FishStick.Player
{
  public class PlayerController
  {
    private int _hp { get; set; }
    private Inventory _inventory;

    private string _currentSceneId;

    public void SetCurrentSceneId(string sceneId)
    {
      _currentSceneId = sceneId;
    }

    public string GetCurrentSceneId()
    {
      return _currentSceneId;
    }

    public PlayerController(int hp, string startingSceneId)
    {
      _currentSceneId = startingSceneId;
      _hp = hp;
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

    public int GetHp()
    {
      return _hp;
    }

    public int RollHp()
    {
      _hp = DiceRoller.Roll("4d6");
      return _hp;
    }

    public int TakeDamage(int amount)
    {
      _hp -= amount;
      return _hp;
    }

    public int Heal(int amount)
    {
      _hp += amount;
      return _hp;
    }
  }
}
