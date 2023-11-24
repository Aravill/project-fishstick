using FishStick.Item;
using Scene;

namespace FishStick.Scene
{
  public interface IScene
  {
    string Id { get; }
    string Description { get; }

    List<IItem> Items { get; }

    List<IElement> Elements { get; }
    List<ITransition> Transitions { get; }

    IItem? GetItem(string itemId);
    IElement? GetElement(string elementId);

    IInteractable? GetElementByName(string target);
    ITransition? GetTransition(string exitName);
  }
}

