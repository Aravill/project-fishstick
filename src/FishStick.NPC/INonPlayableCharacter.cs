using Dialogue;

namespace NPC
{
  interface INonPlayableCharacter
  {
    string Id { get; }

    string Name { get; }

    bool Hostile { get; }

    int HP { get; }

    void TakeDamage(int damage);
    void Heal(int health);
    string SceneDescription { get; }
    List<IDialogue> Dialogues { get; }
    IDialogue CurrentDialogue { get; }
  }
}