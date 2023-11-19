using FishStick.Item;

namespace FishStick.Scene
{
  public interface IScene
  {
    string Id { get; }
    string Description { get; }

    List<IItem> Items { get; }
    List<ITransition> Transitions { get; }

    IItem? GetItem(string itemId);
    ITransition? GetTransition(string exitName);
  }
}

