namespace Scene
{
  public interface IInteractable : IElement
  {
    string Name { get; }

    string Command { get; }
    string OnInteract { get; }

    string[] Args { get; }
  }
}