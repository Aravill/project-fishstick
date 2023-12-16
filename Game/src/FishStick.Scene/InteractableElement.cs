using System.Runtime.Serialization;

namespace Scene
{
  [DataContract]
  public class InteractableElement : IInteractable
  {
    public InteractableElement(
      string id,
      string command,
      string name,
      string onInteract,
      string[] args,
      string sceneDescription,
      bool hidden
    )
    {
      Id = id;
      Name = name;
      OnInteract = onInteract;
      Args = args;
      SceneDescription = sceneDescription;
      Hidden = hidden;
      Command = command;
    }

    [DataMember]
    public string Name { get; }

    [DataMember]
    public string Command { get; }

    [DataMember]
    public string OnInteract { get; }

    [DataMember]
    public string[] Args { get; }

    [DataMember]
    public string Id { get; }
    [DataMember]
    public string SceneDescription { get; }
    [DataMember]
    public bool Hidden { get; set; }
  }
}
