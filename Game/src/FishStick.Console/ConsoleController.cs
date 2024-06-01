using FishStick.Extensions;
using FishStick.Scene;
using FishStick.Session;
using Render;

namespace FishStick.Render
{
  class ConsoleController
  {
    public static void WriteText(string text)
    {
      string withoutTags = text.RemoveTags();

      ConsoleWriter
        .Write(withoutTags)
        .Slowly()
        .Color(ConsoleColor.DarkGray)
        .WithHighlighting(text.FindTaggedWords(), ConsoleColor.DarkYellow)
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
