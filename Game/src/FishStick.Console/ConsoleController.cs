using FishStick.Scene;
using FishStick.Session;
using Render;

namespace FishStick.Render
{
  class ConsoleController
  {
    public static void WriteText(string text)
    {
      string withoutTags = text.FindTaggedWords(out var taggedWords).RemoveTagMarkers();

      ConsoleWriter
        .Write(withoutTags)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords, ConsoleColor.DarkYellow)
        .ToConsole();

      Console.WriteLine();
    }

    public static void DescribeScene(IScene scene)
    {
      SceneStrategy.DescribeScene(scene);
    }

    internal static string ReadCommand(SessionHistory sessionHistory)
    {
      return CommandStrategy.ReadCommand(sessionHistory);
    }
  }
}
