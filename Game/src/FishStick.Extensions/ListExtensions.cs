using Scene;

namespace FishStick.Extensions
{
  public static class ListExtensions
  {
    public static IEnumerable<string> Descriptions<T>(this IEnumerable<T> inputList) where T : ISceneDescribable
      => inputList.Select(item => item.SceneDescription);
    public static IEnumerable<T> Visible<T>(this IEnumerable<T> inputList) where T : IHiddeable
      => inputList.Where(i => !i.Hidden);
  }
}
