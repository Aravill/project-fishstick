using FishStick.Scene;
using FishStick.Session;
using Render;

namespace FishStick.Render
{
  class ConsoleController
  {
    public class GameCursor
    {
      public char cursorSymbol = '▮';
      public int cursorIndex = 0;
    }

    public static void WriteText(string text)
    {
      string withoutTags = text.FindTaggedWords(out var taggedWords).RemoveTagMarkers();

      ConsoleWriter
        .Write(withoutTags)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords.ToDictionary(tag => tag, tag => ConsoleColor.DarkYellow))
        .ToConsole();

      Console.WriteLine();
    }

    public static void DescribeScene(IScene scene)
    {
      SceneStrategy.DescribeScene(scene);
    }

    public static string ReadCommand(SessionHistory history)
    {
      return CommandStrategy.ReadCommand(history);
    }
  }
}
