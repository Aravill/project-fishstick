using FishStick.Render;
using FishStick.Extensions;
using FishStick.Scene;

namespace Render
{
  public static class SceneStrategy
  {
    public static void DescribeScene(IScene scene)
    {
      ConsoleWriter
        .Write(scene.CompleteDescription)
        .NoTags()
        .Slowly()
        .Color(ConsoleColor.DarkGray)
        .WithHighlighting(scene.GetHighlitghts())
        .ToConsole();

      Console.WriteLine();
    }
  }
}
