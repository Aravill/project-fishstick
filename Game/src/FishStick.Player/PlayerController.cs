using FishStick.Dice;
using FishStick.Item;

namespace FishStick.Player
{
  class PlayerController
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

    public PlayerController(int hp)
    {
      _currentSceneId = "0";
      _hp = hp;
      _inventory = new Inventory();
    }

    public void TakeItem(IItem item)
    {
      _inventory.AddItem(item);
    }

    public void RemoveItem(IItem item)
    {
      _inventory.RemoveItem(item);
    }

    public List<IItem> GetInventory()
    {
      return _inventory.GetItems();
    }

    public IItem? GetInventoryItem(string name)
    {
      return _inventory.GetItem(name);
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
