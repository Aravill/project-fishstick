using FishStick.Item;
using FishStick.Scene;

namespace FishStick.Render
{
  class ConsoleController
  {

    public static void WriteText(string text)
    {
      WriteSlowly(text);
      Console.WriteLine();
    }
    public static void DescribeScene(IScene scene)
    {
      WriteSlowly(scene.Description + " "); // Add a space to the end of the line
      foreach (ITransition transition in scene.Transitions)
      {
        WriteSlowly(transition.Description + " ");
      }
      foreach (IItem item in scene.Items)
      {
        WriteSlowly(item.SceneDescription + " ");
      }
      Console.WriteLine();
    }

    public static string ReadCommand()
    {
      string? commandText = null;
      while (commandText == null)
      {
        commandText = Console.ReadLine();
        if (commandText == null)
        {
          continue;
        }
      }
      return commandText;
    }

    private static void WriteSlowly(string message)
    {
      Console.ForegroundColor = ConsoleColor.DarkGray;
      foreach (char c in message)
      {

        // TODO: It would be nice to let the user press a button like space to
        // skip the slow typing effect. For that, this typing effect should
        // probably be in a separate thread.
        // Could use something like this https://stackoverflow.com/questions/62610803/c-sharp-manually-stopping-an-asynchronous-for-statement-typewriter-effect
        Console.Write(c);
        Thread.Sleep(20);
      }
      Console.ForegroundColor = ConsoleColor.White;
    }
  }
}