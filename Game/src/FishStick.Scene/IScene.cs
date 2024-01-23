using FishStick.Item;
using NPC;
using Scene;

namespace FishStick.Scene
{
  public interface IScene
  {
    string Id { get; }
    string Description { get; }

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

    // this is def needed for at least the autocompletion context
    // this content is a concern of this class, though, this data is public so maybe the function that enumerates it could also be elsewhere? dunno
    // feel like world controller is a better place for this, but I'll leave it here for now
    List<string> GetSceneItemsNames();
    List<string> GetSceneNPCsNames();
    List<string> GetSceneInteractableElementsNames();
  }
}
