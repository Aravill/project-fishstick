namespace Scene
{
  public interface IInteractable : IElement
  {
    string Name { get; }

    string Target { get; }
    string OnInteract { get; }

    string[] Args { get; }
  }
}