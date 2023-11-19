namespace FishStick.Exception
{
  // Thrown when a transition is not found in a scene using a given name.
  public class CommandNotFoundException : System.Exception
  {
    public CommandNotFoundException() { }
    public CommandNotFoundException(string message) : base(message) { }
    public CommandNotFoundException(string message, System.Exception inner) : base(message, inner) { }
  }
}