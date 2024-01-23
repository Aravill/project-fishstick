using FishStick.Item;
using NPC;
using Scene;

namespace FishStick.Scene
{
  public class BaseScene(
    string id,
    string description,
    List<ITransition> exits,
    List<IItem> items,
    List<IElement> elements,
    List<INonPlayableCharacter> npcs
  ) : IScene
  {
    public string Id { get; } = id;
    public string Description { get; } = description;
    public List<ITransition> Transitions { get; } = exits;
    public List<IItem> Items { get; set; } = items;

    public List<IElement> Elements { get; set; } = elements;

    public List<INonPlayableCharacter> NPCs { get; set; } = npcs;

    IElement? IScene.GetElement(string elementId)
    {
      return Elements.Find(element => element.Id == elementId);
    }

    IInteractable? IScene.GetElementByName(string name)
    {
      var interactableType = typeof(IInteractable);
      IElement? element = Elements.Find(
        element => element is IInteractable && ((IInteractable)element).Name == name
      );
      if (element == null)
        return null;
      return (IInteractable)element;
    }

    IItem? IScene.GetItem(string itemId)
    {
      return Items.Find(item => item.Id == itemId);
    }

    INonPlayableCharacter? IScene.GetNPC(string npcId)
    {
      return NPCs.Find(npc => npc.Id == npcId);
    }

    INonPlayableCharacter? IScene.GetNPCByName(string npcName)
    {
      return NPCs.Find(npc => npc.Name == npcName);
    }

    ITransition? IScene.GetTransition(string exitName)
    {
      return Transitions.Find(exit => exit.Name == exitName);
    }

    List<string> IScene.GetSceneItemsNames()
    {
        return Items.Select(item => item.Name).ToList();
    }

    List<string> IScene.GetSceneNPCsNames()
    {
      return NPCs.Select(npc => npc.Name).ToList();
    }

    List<string> IScene.GetSceneInteractableElementsNames()
    {
      var interactables = Elements.Where(elem => elem is IInteractable);
      return interactables.Select(elem => ((IInteractable)elem).Name).ToList();
    }
  }
}
