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
      var description = scene.Description;
      var transitions = scene.Transitions.Select(t => t.Description);
      var items       = scene.Items.Where(i => !i.Hidden).Select(i => i.SceneDescription);
      var elements    = scene.Elements.Where(e => !e.Hidden).Select(e => e.SceneDescription);

      var textList = description.Concat(transitions)
                                .Concat(items)
                                .Concat(elements);

      var allText = string.Join(' ', textList)
                          .FindTaggedWords(out var taggedWords)
                          .RemoveTagMarkers();

      ConsoleWriter.Write(allText)
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords, ConsoleColor.DarkYellow)
        .ToConsole();

      Console.WriteLine();
    }

    internal static string ReadCommand(SessionHistory sessionHistory)
    {
      return ConsoleReader.ReadCommand(sessionHistory);
    }
  }
}
