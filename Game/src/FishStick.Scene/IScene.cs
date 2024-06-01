using Character;
using FishStick.Item;
using Scene;
using FishStick.Extensions;

namespace FishStick.Scene
{
  public interface IScene
  {
    string Id { get; }
    string Description { get; }

    List<IItem> Items { get; }

    List<IElement> Elements { get; }

    List<NPC> NPCs { get; }
    List<ITransition> Transitions { get; }

    IItem? GetItem(string itemId);
    IElement? GetElement(string elementId);

    IInteractable? GetElementByName(string target);
    ITransition? GetTransition(string exitName);

    NPC? GetNPC(string npcId);
    NPC? GetNPCByName(string npcId);
    
    /// <summary>
    /// Complete description including scene decription, transitions, visible items, visible elements and npcs.
    /// </summary>
    /// <returns>
    /// A complete desctiption <see cref="System.String"/> joined by " " (space) character.
    /// </returns>
    public string CompleteDescription 
      => string.Join(" ", Description.Concat(Transitions.Descriptions())
                                     .Concat(Items.Visible().Descriptions())
                                     .Concat(Elements.Visible().Descriptions())
                                     .Concat(NPCs.Descriptions()));

    public Dictionary<string, ConsoleColor> GetHighlitghts(ConsoleColor highlighColor = ConsoleColor.DarkYellow)
      => CompleteDescription.FindTaggedWords().ToDictionary(word => word, word => highlighColor);
  }
}
