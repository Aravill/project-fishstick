using Dialogue;

namespace NPC
{
  public interface INonPlayableCharacter
  {
    string Id { get; }

    string Name { get; }
    string SceneDescription { get; }
    List<IDialogue> Dialogues { get; }
  }
}
