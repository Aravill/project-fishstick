namespace Scene
{
  public class InteractableElement : IInteractable
  {
    public InteractableElement(string id, string name, string target, string onInteract, string[] args, string sceneDescription, bool hidden)
    {
      Id = id;
      Name = name;
      OnInteract = onInteract;
      Args = args;
      SceneDescription = sceneDescription;
      Hidden = hidden;
      Target = target;
    }
    public string Name { get; }

    public string Target { get; }

    public string OnInteract { get; }

    public string[] Args { get; }

    public string Id { get; }

    public string SceneDescription { get; }

    public bool Hidden { get; set; }

  }
}