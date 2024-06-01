using FishStick.Render;
using FishStick.Scene;

namespace Render
{
  public static class SceneStrategy
  {
    public static void DescribeScene(IScene scene)
    {
      ConsoleWriter
        .Write(
          BuildDescriptionString(scene)
            .FindTaggedWords(out var taggedWords)
            .RemoveTagMarkers())
        .Slowly()
        .WithColor(ConsoleColor.DarkGray)
        .WithHighlighting(taggedWords, ConsoleColor.DarkYellow)
        .ToConsole();

      Console.WriteLine();
    }

    private static string BuildDescriptionString(IScene scene)
    {
      string description = scene.Description;
      IEnumerable<string> transitions = MapSceneTransitions(scene);
      IEnumerable<string> items = MapSceneItems(scene);
      IEnumerable<string> elements = MapSceneElements(scene);
      IEnumerable<string> npcs = MapSceneNPCs(scene);

      IEnumerable<string> concatenated = description
        .Concat(transitions)
        .Concat(items)
        .Concat(elements)
        .Concat(npcs);

      return string.Join(" ", concatenated);
      ;
    }

    private static IEnumerable<string> MapSceneTransitions(IScene scene)
    {
      return scene.Transitions.Select(t => t.Description);
    }

    private static IEnumerable<string> MapSceneItems(IScene scene)
    {
      return scene.Items.Where(i => !i.Hidden).Select(i => i.SceneDescription);
    }

    private static IEnumerable<string> MapSceneElements(IScene scene)
    {
      return scene.Elements.Where(e => !e.Hidden).Select(e => e.SceneDescription);
    }

    private static IEnumerable<string> MapSceneNPCs(IScene scene)
    {
      return scene.NPCs.Select(npc => npc.SceneDescription);
    }
  }
}
