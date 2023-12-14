using Dialogue;

namespace NPC
{
  class NonPlayableCharacter : INonPlayableCharacter
  {
    public string Id { get; }

    public string Name { get; }

    public string SceneDescription { get; }

    public List<IDialogue> Dialogues { get; }

    public NonPlayableCharacter(
      string id,
      string name,
      string sceneDescription,
      List<IDialogue> dialogues
    )
    {
      Id = id;
      Name = name;
      SceneDescription = sceneDescription;
      Dialogues = dialogues;
    }
  }
}
