namespace FishStick.Exception
{
  // Thrown when a transition is not found in a scene using a given name.
  public class TransitionNotFoundException : System.Exception
  {
    public TransitionNotFoundException() { }
    public TransitionNotFoundException(string message) : base(message) { }
    public TransitionNotFoundException(string message, System.Exception inner) : base(message, inner) { }
  }
}