namespace FishStick.Exception
{
  // Thrown when a transition is not found in a scene using a given name.
  public class ItemUnspecifiedException : System.Exception
  {
    public ItemUnspecifiedException() { }
    public ItemUnspecifiedException(string message) : base(message) { }
    public ItemUnspecifiedException(string message, System.Exception inner) : base(message, inner) { }
  }
}