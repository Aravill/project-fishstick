namespace FishStick.Exception
{
  // Thrown when a transition is not found in a scene using a given name.
  public class ItemNotFoundException : System.Exception
  {
    public ItemNotFoundException() { }
    public ItemNotFoundException(string message) : base(message) { }
    public ItemNotFoundException(string message, System.Exception inner) : base(message, inner) { }
  }
}