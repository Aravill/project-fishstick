using FishStick.Item;

namespace FishStick.Scene
{

  public class BaseScene(string id, string description, List<ITransition> exits, List<IItem> items) : IScene
  {
    public string Id { get; } = id;
    public string Description { get; } = description;
    public List<ITransition> Transitions { get; } = exits;
    public List<IItem> Items { get; set; } = items;


    IItem? IScene.GetItem(string itemId)
    {
      return Items.Find(item => item.Id == itemId);
    }

    ITransition? IScene.GetTransition(string exitName)
    {
      return Transitions.Find(exit => exit.Name == exitName);
    }

  }
}
