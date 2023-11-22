
namespace FishStick.AssetData
{
  public class InteractableElementData : ElementData
  {
    public string Name { get; set; }

    public string Target { get; set; }
    public string OnInteract { get; set; }

    public string[] Args { get; set; }
    public InteractableElementData(string id, string sceneDescription, bool hidden, string type, string inScene, string name, string target, string onInteract, string[] args) : base(id, sceneDescription, hidden, type, inScene)
    {
      Name = name;
      OnInteract = onInteract;
      Args = args;
      Target = target;
    }

  }
}
