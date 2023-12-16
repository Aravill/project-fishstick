using System.Runtime.Serialization;
using Dialogue;

namespace NPC
{
  [DataContract]
  class NonPlayableCharacter : INonPlayableCharacter
  {
    [DataMember]
    public string Id { get; }
    [DataMember]
    public string Name { get; }
    [DataMember]
    public string SceneDescription { get; }
    [DataMember]
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
