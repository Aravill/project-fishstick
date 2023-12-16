using FishStick.Item;
using NPC;
using Scene;

namespace FishStick.Scene
{
  public interface IScene
  {
    string Id { get; }
    string Description { get; }

    // TODO: These following properties should not be on IScene
    // The very basic scene does not require items, elements, npcs or transitions
    // These should be moved to a more specific class, like InteractiveScene (now BaseScene)
    List<IItem> Items { get; }

    List<IElement> Elements { get; }

    List<INonPlayableCharacter> NPCs { get; }
    List<ITransition> Transitions { get; }

    IItem? GetItem(string itemId);
    IElement? GetElement(string elementId);

    IInteractable? GetElementByName(string target);
    ITransition? GetTransition(string exitName);

    INonPlayableCharacter? GetNPC(string npcId);
    INonPlayableCharacter? GetNPCByName(string npcId);
  }
}
