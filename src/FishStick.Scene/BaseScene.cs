using FishStick.Item;
using Scene;

namespace FishStick.Scene
{

  public class BaseScene(string id, string description, List<ITransition> exits, List<IItem> items, List<IElement> elements) : IScene
  {
    public string Id { get; } = id;
    public string Description { get; } = description;
    public List<ITransition> Transitions { get; } = exits;
    public List<IItem> Items { get; set; } = items;

    public List<IElement> Elements { get; set; } = elements;

    IElement? IScene.GetElement(string elementId)
    {
      return Elements.Find(element => element.Id == elementId);
    }

    IInteractable? IScene.GetElementByCommandName(string commandName)
    {
      var interactableType = typeof(IInteractable);
      IElement? element = Elements.Find(element => element is IInteractable && ((IInteractable)element).Name == commandName);
      if (element == null) return null;
      return (IInteractable)element;
    }
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
